namespace RandomUserSharp.Models
{
    public class PasswordOptions
    {
        public bool UseSpecialCharacters { get; set; } = true;
        public bool UseUpperCaseCharacters { get; set; } = true;
        public bool UseLowerCaseCharacters { get; set; } = true;
        public bool UseNumbers { get; set; } = true;
        public long MinLength { get; set; } = 8;
        public long MaxLength { get; set; } = 64;

        public override string ToString()
        {
            string passwordString = string.Empty;

            if (UseSpecialCharacters)
            {
                passwordString += "special,";
            }

            if (UseUpperCaseCharacters)
            {
                passwordString += "upper,";
            }

            if (UseLowerCaseCharacters)
            {
                passwordString += "lower,";
            }

            if (UseNumbers)
            {
                passwordString += "number,";
            }

            passwordString += $"{MinLength}-{MaxLength}";

            return passwordString;
        }
    }
}
