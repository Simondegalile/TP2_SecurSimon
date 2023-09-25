using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace TP2_SecurSimon.Models
{
    public class UserDao
    {
        private string _filePath;
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
