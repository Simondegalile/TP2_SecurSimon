using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TP2_SecurSimon.Models;
using TP2_SecurSimon.Services; 
using TP2_SecurSimon.Views;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

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
        public Command LoadCredentialsCommand { get; }  
        public Command AddCredentialsCommand { get; }  
        public Command<Credentials> CredentialsTapped { get; }  // Changement du nom et du type


        public ItemsViewModel()
        {
            Title = "Browse";
            CredentialsList = new ObservableCollection<Credentials>();
            LoadCredentialsCommand = new Command(async () => await ExecuteLoadCredentialsCommand());
            CredentialsTapped = new Command<Credentials>(OnCredentialsSelected);
            daoCredentials = new Dao_Credentials();
        }

        async Task ExecuteLoadCredentialsCommand()
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedCredentials = null; 
        }

        public Credentials SelectedCredentials 
        {
            get => _selectedCredentials;
            set
            {
                SetProperty(ref _selectedCredentials, value);
                OnCredentialsSelected(value);  
            }
        }

        async void OnCredentialsSelected(Credentials credentials) 
        {
            if (credentials == null)
            return;
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

                daoCredentials.AddCredential(credential); 
                CredentialsList.Add(credential);  
                PopupNavigation.Instance.PopAsync(true);
            }
        }
    }
}
