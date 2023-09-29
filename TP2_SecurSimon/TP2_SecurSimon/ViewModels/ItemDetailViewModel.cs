using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TP2_SecurSimon.Models;
using TP2_SecurSimon.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace TP2_SecurSimon.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private readonly Dao_Credentials daoCredentials; // Utilisation de readonly car il est initialisé une seule fois

        public ObservableCollection<Credentials> CredentialsList { get; }

        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get => itemId;
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        // Constructeur pour initialiser les propriétés et charger les données
        public ItemDetailViewModel()
        {
            daoCredentials = new Dao_Credentials();
            CredentialsList = new ObservableCollection<Credentials>();
            LoadCredentialsAsync();
        }

        // Méthode pour charger un élément par son ID
        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Échec du chargement de l'élément");
            }
        }

        // Méthode pour charger tous les credentials
        private async Task LoadCredentialsAsync()
        {
            try
            {
                CredentialsList.Clear();
                var credentials = await daoCredentials.GetAllCredentialsAsync();
                foreach (var cred in credentials)
                {
                    CredentialsList.Add(cred);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors du chargement des credentials : {ex.Message}");
            }
        }
    }
}
