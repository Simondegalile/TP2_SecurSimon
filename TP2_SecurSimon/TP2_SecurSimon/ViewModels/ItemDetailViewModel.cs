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
        public string Id { get; set; }

        private Dao_Credentials daoCredentials;
        public ObservableCollection<Credentials> CredentialsList { get; }



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
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

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
                Debug.WriteLine("Failed to Load Item");
            }
        }
        public ItemDetailViewModel()
        {
            daoCredentials = new Dao_Credentials(); // Initialisation de la variable daoCredentials
            CredentialsList = new ObservableCollection<Credentials>();
            LoadCredentialsAsync(); // Appel à la méthode pour charger les credentials
        }

        private async Task LoadCredentialsAsync()
        {
            try
            {
                CredentialsList.Clear();
                var credentials = await daoCredentials.GetAllCredentialsAsync(); // Utilisation de daoCredentials
                foreach (var cred in credentials)
                {
                    CredentialsList.Add(cred);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
