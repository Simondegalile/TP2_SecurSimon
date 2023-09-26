using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        HttpClient _client ;
        public UserDao(string filePath)
        {
            _filePath = filePath;
            _client = new HttpClient();        
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



        public async Task<List<User>> GetAllUser()
        {
            string url = "http://10.0.2.2:8080/getAlluser";

            Uri uri = new Uri(url);
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<List<User>>(content);


                    return (user);
                }
                return (null);
            }
            catch (Exception ex)
            {              
                return (null);
            }
        }
        public async Task<bool> AddUser(User user)
        {
            string url = "http://10.0.2.2:8080/AddUser";
            Uri uri = new Uri(url);

            var content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }




        public async Task<User> GetUserByEmailFromApi(string email)
        {
            List<User> allUsers = await GetAllUser();
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
