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
        // Liste pour stocker les credentials
        public List<Credentials> credentialsList;

        // Constructeur pour initialiser la liste des credentials
        public Dao_Credentials()
        {
            credentialsList = new List<Credentials>();
        }

        // Méthode pour obtenir tous les credentials de manière asynchrone
        public async Task<List<Credentials>> GetAllCredentialsAsync()
        {
            string url = "http://almeida.alwaysdata.net/getAllCredential";
            Uri uri = new Uri(url);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    credentialsList = JsonConvert.DeserializeObject<List<Credentials>>(content);
                    return credentialsList;
                }
                else
                {
                    // En cas d'erreur, retourner une liste vide
                    return new List<Credentials>();
                }
            }
        }

        // Méthode pour obtenir tous les credentials
        public List<Credentials> GetAllCredentials()
        {
            return credentialsList;
        }

        // Méthode pour ajouter un credential à la liste locale
        public void AddCredential(Credentials credential)
        {
            credentialsList.Add(credential);
        }

        // Méthode pour ajouter un credential de manière asynchrone
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
                    // Si succès, gérer ici
                }
                else
                {
                    // Si erreur, gérer ici
                }
            }
        }

        // Méthode pour supprimer un credential de manière asynchrone
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
                        return true; // Suppression réussie
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans DeleteCredentialAsync: {ex.Message}");
            }
            return false; // Échec de la suppression ou une erreur s'est produite
        }
    }
}
