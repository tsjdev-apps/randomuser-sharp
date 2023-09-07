using RandomUserSharp.Models;
using System.Collections.Generic;
using System.Linq;

namespace RandomUserSharp.Utils
{
    internal class RequestOptions
    {
        public int Count { get; set; }
        public Gender Gender { get; set; }
        public string Seed { get; set; }
        public bool UseLegoImages { get; set; }
        public IEnumerable<Nationality> Nationalities { get; set; }
        public PasswordOptions PasswordOptions { get; set; }

        public override string ToString()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if (Count > 1)
            {
                parameters.Add("results", Count < 5000 ? Count.ToString() : "5000");
            }

            if (Gender != Gender.Both)
            {
                parameters.Add("gender", Gender.ToString().ToLower());
            }

            if (!string.IsNullOrEmpty(Seed))
            {
                parameters.Add("seed", Seed);
            }

            if (Nationalities != null && !Nationalities.Contains(Nationality.ALL) && !Nationalities.Contains(Nationality.LEGO))
            {
                parameters.Add("nat", string.Join(",", Nationalities.Except(new List<Nationality> { Nationality.ALL, Nationality.LEGO }).Select(p => p)));
            }

            if (PasswordOptions != null)
            {
                parameters.Add("password", PasswordOptions.ToString());
            }

            string parameterString = string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"));

            if (UseLegoImages)
            {
                parameterString += "&lego";
            }

            return parameterString;
        }
    }
}
