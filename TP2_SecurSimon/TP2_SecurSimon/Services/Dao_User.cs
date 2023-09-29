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
        private readonly string _filePath;
        private readonly HttpClient _client;

        // Constructeur pour initialiser le chemin du fichier et le client HTTP
        public UserDao(string filePath)
        {
            _filePath = filePath;
            _client = new HttpClient();
        }

        // Méthode pour lire les utilisateurs depuis un fichier
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

        // Méthode pour obtenir l'email d'un utilisateur par son ID
        public string GetEmail(int userId)
        {
            var users = ReadUsers();
            return users.FirstOrDefault(u => u.id == userId)?.email;
        }

        // Méthode pour obtenir tous les utilisateurs depuis l'API
        public async Task<List<User>> GetAllUser()
        {
            string url = "http://almeida.alwaysdata.net/getAlluser";
            Uri uri = new Uri(url);
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<List<User>>(content);
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Méthode pour ajouter un utilisateur via l'API
        public async Task<bool> AddUser(User user)
        {
            string url = "http://almeida.alwaysdata.net/AddUser";
            Uri uri = new Uri(url);

            var content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, content).ConfigureAwait(false);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Méthode pour obtenir un utilisateur par son email depuis l'API
        public async Task<User> GetUserByEmailFromApi(string email)
        {
            List<User> allUsers = await GetAllUser();
            return allUsers?.FirstOrDefault(u => u.email == email);
        }

        // Méthode pour obtenir tous les utilisateurs depuis le fichier
        public List<User> GetAllUsers()
        {
            return ReadUsers();
        }

        // Méthode pour obtenir un utilisateur par son email depuis le fichier
        public User GetUserByEmail(string email)
        {
            var users = ReadUsers();
            return users.FirstOrDefault(u => u.email == email);
        }
    }

    // Classe pour envelopper la liste des utilisateurs lors de la désérialisation
    class UsersWrapper
    {
        public List<User> utilisateurs { get; set; }
    }
}
