using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TP2_SecurSimon.Models;
using TP2_SecurSimon.Services;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System.Linq;
using TP2_SecurSimon.Views;

namespace TP2_SecurSimon.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        // Propriétés pour stocker les informations des credentials
        public string Website { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        private Credentials _selectedCredentials;  // Credential actuellement sélectionné
        private readonly Dao_Credentials daoCredentials;  // Service pour interagir avec la base de données des credentials

        // Commandes pour gérer les interactions utilisateur
        public Command<int> DeleteCommand { get; }
        public Command<Credentials> EditCredentialsCommand { get; }
        public Command LoadCredentialsCommand { get; }
        public Command AddCredentialsCommand { get; }
        public Command<Credentials> CredentialsTapped { get; }

        // Liste des credentials
        public ObservableCollection<Credentials> CredentialsList { get; }

        // Constructeur
        public ItemsViewModel()
        {
            Title = "Browse";
            CredentialsList = new ObservableCollection<Credentials>();
            LoadCredentialsCommand = new Command(async () => await ExecuteLoadCredentialsCommand());
            CredentialsTapped = new Command<Credentials>(OnCredentialsSelected);
            EditCredentialsCommand = new Command<Credentials>(OnEditCredentials);
            DeleteCommand = new Command<int>(async (id) => await OnSupprItem(id));
            daoCredentials = new Dao_Credentials();
        }

        // Méthode pour charger tous les credentials
        private async Task ExecuteLoadCredentialsCommand()
        {
            IsBusy = true;

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
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Méthode pour supprimer un credential
        private async Task OnSupprItem(int id)
        {
            bool isDeleted = await daoCredentials.DeleteCredentialAsync(id);
            if (isDeleted)
            {
                var itemToRemove = CredentialsList.FirstOrDefault(i => i.Id == id);
                if (itemToRemove != null)
                    CredentialsList.Remove(itemToRemove);
            }
        }

        // Méthode appelée lors de l'apparition de la vue
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedCredentials = null;
        }

        // Méthode pour éditer un credential
        private async void OnEditCredentials(Credentials credentials)
        {
            var editPage = new EditCredentialPopup(credentials);
            await PopupNavigation.Instance.PushAsync(editPage);
        }

        // Propriété pour gérer le credential sélectionné
        public Credentials SelectedCredentials
        {
            get => _selectedCredentials;
            set
            {
                SetProperty(ref _selectedCredentials, value);
                OnCredentialsSelected(value);
            }
        }

        // Méthode appelée lors de la sélection d'un credential
        private void OnCredentialsSelected(Credentials credentials)
        {
            if (credentials == null)
                return;
        }

        // Méthode pour ajouter un nouveau credential
        public void OnAddClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Website) && !string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
            {
                var credential = new TP2_SecurSimon.Models.Credentials
                {
                    Website = Website,
                    User = User,
                    Password = Password
                };

                daoCredentials.AddCredential(credential);
                CredentialsList.Add(credential);
                PopupNavigation.Instance.PopAsync(true);
            }
        }
    }
}
