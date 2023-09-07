using RandomUserSharp.Models;

namespace RandomUserSharp.UnitTests
{
    public class RandomUserClientTests
    {
        private RandomUserClient _randomUserClient;

        public RandomUserClientTests()
        {
            _randomUserClient = new RandomUserClient();
        }

        [Fact]
        public async Task RandomUserClient_GetRandomUsersAsync_NoInput()
        {
            var users = await _randomUserClient.GetRandomUsersAsync();

            Assert.NotNull(users);
            Assert.Single(users);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(100)]
        public async Task RandomUserClient_GetRandomUsersAsync_GetCorrespondingNumberOfUsers(int cnt)
        {
            var users = await _randomUserClient.GetRandomUsersAsync(cnt);

            Assert.NotNull(users);
            Assert.Equal(cnt, users.Count);
        }

        [Fact]
        public async Task RandomUserClient_GetRandomUsersAsync_AllFieldsAreFilled()
        {
            var user = (await _randomUserClient.GetRandomUsersAsync(1)).FirstOrDefault();

            Assert.NotNull(user);
            Assert.NotNull(user.Name);
            Assert.NotNull(user.Cell);
            Assert.NotNull(user.Email);
            Assert.NotNull(user.Phone);
            Assert.NotNull(user.DateOfBirth);
            Assert.NotNull(user.Location);
            Assert.NotNull(user.Location.Postcode);
            Assert.NotNull(user.Location.City);
            Assert.NotNull(user.Location.Country);
            Assert.NotNull(user.Location.State);
            Assert.NotNull(user.Location.Street);
            Assert.NotNull(user.Location.Coordinates);
            Assert.NotNull(user.Location.Coordinates.Latitude);
            Assert.NotNull(user.Location.Coordinates.Longitude);
            Assert.NotNull(user.Location.Timezone);
            Assert.NotNull(user.Location.Timezone.Description);
            Assert.NotNull(user.Location.Timezone.Offset);
            Assert.NotNull(user.Id);
            Assert.NotNull(user.Id.Name);
            Assert.NotNull(user.Login);
            Assert.NotNull(user.Login.Md5);
            Assert.NotNull(user.Login.Password);
            Assert.NotNull(user.Login.Salt);
            Assert.NotNull(user.Login.Sha1);
            Assert.NotNull(user.Login.Sha256);
            Assert.NotNull(user.Login.Username);
            Assert.NotNull(user.PictureInfo);
            Assert.NotNull(user.PictureInfo.Large);
            Assert.NotNull(user.PictureInfo.Medium);
            Assert.NotNull(user.PictureInfo.Thumbnail);
            Assert.NotNull(user.Registered);
        }

        [Theory]
        [InlineData(Gender.Male)]
        [InlineData(Gender.Female)]
        public async Task RandomUserClient_GetRandomUsersAsync_RespectGender(Gender gender)
        {
            var users = await _randomUserClient.GetRandomUsersAsync(50, gender);
            var providedGenders = users.Select(u => u.Gender).Distinct().ToList();

            Assert.NotNull(users);
            Assert.Single(providedGenders);
        }

        [Theory]
        [InlineData(Nationality.AU)]
        [InlineData(Nationality.BR)]
        [InlineData(Nationality.NO)]
        public async Task RandomUserClient_GetRandomUsersAsync_RespectNationality(Nationality nationality)
        {
            var users = await _randomUserClient.GetRandomUsersAsync(50, nationalities: new List<Nationality> { nationality });
            var providedNationalities = users.Select(u => u.Nationality).Distinct().ToList();

            Assert.NotNull(users);
            Assert.Single(providedNationalities);
            Assert.Equal(nationality, providedNationalities.First());
        }

        [Fact]
        public async Task RandomUserClient_GetRandomUsersAsync_UseLegoImages()
        {
            var users = await _randomUserClient.GetRandomUsersAsync(50, useLegoImages: true);
            var providedNationalities = users.Select(u => u.Nationality).Distinct().ToList();

            Assert.NotNull(users);
            Assert.Single(providedNationalities);
        }

        [Fact]
        public async Task RandomUserClient_GetRandomUsersAsync_GetOneUserEvenNullIsProvided()
        {
            var users = await _randomUserClient.GetRandomUsersAsync(0);

            Assert.NotNull(users);
            Assert.Single(users);
        }

        [Fact]
        public async Task RandomUserClient_GetRandomUsersAsync_GetSpecificUserBySeed()
        {
            var user = (await _randomUserClient.GetRandomUsersAsync(seed: "53f075655c8f5cc4")).FirstOrDefault();

            Assert.NotNull(user);
            Assert.Equal(Gender.Male, user.Gender);
            Assert.Equal("Mr", user.Name.Title);
            Assert.Equal("Célsio", user.Name.First);
            Assert.Equal("da Luz", user.Name.Last);
        }

        [Fact]
        public async Task RandomUserClient_GetRandomUsersAsync_SpecifiedPasswordRequestOnlyNumbers()
        {
            var user = (await _randomUserClient.GetRandomUsersAsync(passwordOptions: new PasswordOptions { MinLength = 6, MaxLength = 6, UseLowerCaseCharacters = false, UseUpperCaseCharacters = false, UseNumbers = true, UseSpecialCharacters = false })).FirstOrDefault();

            Assert.NotNull(user);
            Assert.Equal(6, user.Login.Password.Length);
            Assert.True(int.TryParse(user.Login.Password, out _));
        }

        [Fact]
        public async Task RandomUserClient_GetRandomUsersAsync_SpecifiedPasswordRequestOnlyLowercase()
        {
            var user = (await _randomUserClient.GetRandomUsersAsync(passwordOptions: new PasswordOptions { MinLength = 6, MaxLength = 6, UseLowerCaseCharacters = true, UseUpperCaseCharacters = false, UseNumbers = false, UseSpecialCharacters = false })).FirstOrDefault();

            Assert.NotNull(user);
            Assert.Equal(6, user.Login.Password.Length);
            Assert.Equal(user.Login.Password.ToLower(), user.Login.Password);
        }

        [Fact]
        public async Task RandomUserClient_GetRandomUsersAsync_SpecifiedPasswordRequestOnlyUppercase()
        {
            var user = (await _randomUserClient.GetRandomUsersAsync(passwordOptions: new PasswordOptions { MinLength = 6, MaxLength = 6, UseLowerCaseCharacters = false, UseUpperCaseCharacters = true, UseNumbers = false, UseSpecialCharacters = false })).FirstOrDefault();

            Assert.NotNull(user);
            Assert.Equal(6, user.Login.Password.Length);
            Assert.Equal(user.Login.Password.ToUpper(), user.Login.Password);
        }
    }
}