using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TP2_SecurSimon.Models;

namespace TP2_SecurSimon.Services
{
    public class Dao_Credentials
    {
        public List<Credentials> credentialsList;

        public Dao_Credentials()
        {
            credentialsList = new List<Credentials>(); // Initialiser la liste
        }

        public async Task<List<Credentials>> GetAllCredentialsAsync()
        {
            string url = "http://10.0.2.2:8080/getAllCredential";

            Uri uri = new Uri(url);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri); // Supprimer l'ajout inutile de "getAllCredential"
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    credentialsList = JsonConvert.DeserializeObject<List<Credentials>>(content); // Mettre à jour la liste avec les données reçues
                    return credentialsList;
                }
                else
                {
                    // Gestion des erreurs...
                    return new List<Credentials>();
                }
            }
        }

        // Pour obtenir tous les credentials
        public List<Credentials> GetAllCredentials()
        {
            return credentialsList;
        }

        public void AddCredential(Credentials credential)
        {
            credentialsList.Add(credential);
        }
    }
}
