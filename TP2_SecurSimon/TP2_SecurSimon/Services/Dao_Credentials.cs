using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            string url = "http://almeida.alwaysdata.net/getAllCredential";
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

        public async Task AddCredentialAsync(Credentials credential)
        {
            string url = "http://almeida.alwaysdata.net/AddCredential";
            Uri uri = new Uri(url);
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(credential), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    // Gérer le succès ici
                }
                else
                {
                    // Gérer l'erreur ici
                }
            }
        }
        public async Task<bool> DeleteCredentialAsync(int id)
        {
            try
            {
                Uri uri = new Uri($"http://almeida.alwaysdata.net/deleteCredential/{id}");

                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(uri).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        return true; // Successful deletion
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteCredentialAsync: {ex.Message}");
            }
            return false; // Deletion failed or an error occurred
        }



        
    }
}
