using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TP2_SecurSimon.Models;
using TP2_SecurSimon.Services; // Ajout de cette référence
using TP2_SecurSimon.Views;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;

namespace TP2_SecurSimon.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {


        public string Website { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        private Credentials _selectedCredentials;  // Changement de type
        private Dao_Credentials daoCredentials;    // Instance de Dao_Credentials

        public ObservableCollection<Credentials> CredentialsList { get; }  // Changement du nom et du type
        public Command LoadCredentialsCommand { get; }  // Renommé pour la clarté
        public Command AddCredentialsCommand { get; }  // Renommé pour la clarté
        public Command<Credentials> CredentialsTapped { get; }  // Changement du nom et du type





        public ItemsViewModel()
        {
            Title = "Browse";
            CredentialsList = new ObservableCollection<Credentials>();
            LoadCredentialsCommand = new Command(async () => await ExecuteLoadCredentialsCommand());
            CredentialsTapped = new Command<Credentials>(OnCredentialsSelected);


            daoCredentials = new Dao_Credentials(); // Initialisation
        }
        
        async Task ExecuteLoadCredentialsCommand()
        {
            IsBusy = true;

            try
            {
                CredentialsList.Clear();
                var credentials = daoCredentials.GetAllCredentials();
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedCredentials = null;  // Changement de nom
        }

        public Credentials SelectedCredentials  // Changement du nom et du type
        {
            get => _selectedCredentials;
            set
            {
                SetProperty(ref _selectedCredentials, value);
                OnCredentialsSelected(value);  // Changement de nom
            }
        }

        async void OnCredentialsSelected(Credentials credentials)  // Changement du nom et du type
        {
            if (credentials == null)
                return;

            // Mettez à jour avec la logique appropriée si vous avez une page de détails pour Credentials
            // Par exemple: 
            // await Shell.Current.GoToAsync($"{nameof(CredentialsDetailPage)}?{nameof(CredentialsDetailViewModel.CredentialsId)}={credentials.Id}");
        }
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

                daoCredentials.AddCredential(credential); // Corrected from _daoCredentials
                CredentialsList.Add(credential);  // Corrected from _viewModel.CredentialsList
                PopupNavigation.Instance.PopAsync(true);
            }
        }


    }
}
