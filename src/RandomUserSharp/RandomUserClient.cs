using Newtonsoft.Json;
using RandomUserSharp.Models;
using RandomUserSharp.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RandomUserSharp
{
    public class RandomUserClient : IDisposable, IRandomUserClient
    {
        private readonly HttpClient _httpClient;

        private bool _disposed;

        public RandomUserClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://api.randomuser.me/1.4") };
        }

        public async Task<List<User>> GetRandomUsersAsync(
           int count = 1,
           Gender gender = Gender.Both,
           List<Nationality> nationalities = null,
           bool useLegoImages = false,
           string seed = null,
           PasswordOptions passwordOptions = null)
        {
            RequestOptions options = new RequestOptions { Count = count, Gender = gender, Seed = seed, UseLegoImages = useLegoImages, Nationalities = nationalities, PasswordOptions = passwordOptions };
            return await GetRandomUsersAsync(options).ContinueWith(r => r.Result);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private async Task<List<User>> GetRandomUsersAsync(
            RequestOptions options)
        {
            string parameters = $"/?{options}";
            HttpResponseMessage response = await _httpClient.GetAsync(parameters);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<User> users = await Task.Run(() => JsonConvert.DeserializeObject<RootObject>(json).Users);

            return users;
        }


        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            try
            {
                if (disposing)
                    _httpClient.Dispose();
            }
            finally
            {
                _disposed = true;
            }
        }
    }
}
