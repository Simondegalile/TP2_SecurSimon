using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace TP2_SecurSimon.Models
{
    public class UserDao
    {
        private string _filePath;
        HttpClient _client;
        public UserDao(string filePath)
        {
            _filePath = filePath;
        }

        private List<User> ReadUsers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            var jsonData = File.ReadAllText(_filePath);
            var users = JsonConvert.DeserializeObject<UsersWrapper>(jsonData).utilisateurs;
            return users ?? new List<User>();
        }

        public string GetEmail(int userId)
        {
            var users = ReadUsers();
            return users.FirstOrDefault(u => u.id == userId)?.email;
        }





        public async Task<List<User>> GetClassesAsync(bool forceRefresh = false)
        {
            Uri uri = new Uri(string.Format("http://172.31.254.107:8080" + "/getAlluser", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var User = JsonConvert.DeserializeObject<List<User>>(content);
                    return await Task.FromResult(User);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return null;
        }



        public async Task<User> GetUserByEmailFromApi(string email)
        {
            List<User> allUsers = await GetClassesAsync();
            if (allUsers == null) return null;
            return allUsers.FirstOrDefault(u => u.email == email);
        }


        public List<User> GetAllUsers()
        {
            return ReadUsers();
        }

        public User GetUserByEmail(string email)
        {
            var users = ReadUsers();
            return users.FirstOrDefault(u => u.email == email);
        }
    }
    class UsersWrapper
    {
        public List<User> utilisateurs { get; set; }
    }
}
