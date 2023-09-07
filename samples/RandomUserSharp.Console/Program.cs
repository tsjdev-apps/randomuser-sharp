using RandomUserSharp.Models;
using RandomUserSharp;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<GetUsersCommand>("get-users");
});
return await app.RunAsync(args);

class GetUsersCommand : AsyncCommand<GetUsersCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("-n|--number")]
        [Description("Get the amount of random users")]
        public int? Number { get; init; }

        [CommandOption("-g|--gender")]
        [Description("Select the gender")]
        public Gender? Gender { get; init; }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        AnsiConsole.Write(
            new FigletText("RandomUserSharp")
            .LeftJustified()
            .Color(Color.Red));

        var numberOfUsers = AskNumberOfUsersIfMissing(settings.Number);
        var gender = AskGenderIfMissing(settings.Gender);

        var users = new List<User>();

        await AnsiConsole.Status().StartAsync("Getting random users...", async ctx =>
        {
            users = await GetRandomUsersAsync(numberOfUsers, gender);
        });

        AnsiConsole.MarkupLine($"[green]Here are your {numberOfUsers} random users:[/]");

        var table = new Table()
            .AddColumn(new TableColumn("First Name").LeftAligned())
            .AddColumn(new TableColumn("Last Name").LeftAligned())
            .AddColumn(new TableColumn("Login").Centered())
            .AddColumn(new TableColumn("Age").RightAligned());

        AnsiConsole.Live(table)
            .Start(ctx =>
            {
                foreach (var user in users)
                {
                    table.AddRow(user.Name.First, user.Name.Last, user.Login.Username, user.DateOfBirth.Age.ToString());
                    ctx.Refresh();
                    Thread.Sleep(750);
                }
            });

        return 0;

        static int AskNumberOfUsersIfMissing(int? current)
            => current ?? AnsiConsole.Prompt(
                new TextPrompt<int>("Enter number of random users: ")
                .Validate(number => number > 0 && number < 100 ? ValidationResult.Success()
                : ValidationResult.Error("[yellow]Invalid number[/]")));

        static Gender AskGenderIfMissing(Gender? current)
            => current ?? AnsiConsole.Prompt(
                new SelectionPrompt<Gender>()
                .Title("Select the gender.")
                .PageSize(3)
                .AddChoices(new[] { Gender.Both, Gender.Female, Gender.Male }));

        static async Task<List<User>> GetRandomUsersAsync(int numberOfUsers, Gender gender)
            => await new RandomUserClient().GetRandomUsersAsync(numberOfUsers, gender, new List<Nationality> { Nationality.GB, Nationality.DE });
    }
}