using Rg.Plugins.Popup.Services;
using System;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2_SecurSimon.Services; 

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TP2_SecurSimon.ViewModels;

namespace TP2_SecurSimon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCredentialPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private Dao_Credentials _daoCredentials;
        private ItemsViewModel _viewModel;

        public AddCredentialPopup(Dao_Credentials daoCredentials)
        {
            InitializeComponent();
            _daoCredentials = daoCredentials;

        }
        public AddCredentialPopup(Dao_Credentials daoCredentials, ItemsViewModel viewModel)
        {
            InitializeComponent();
            _daoCredentials = daoCredentials;
            _viewModel = viewModel; // store the passed ViewModel.
        }

        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddCredentialPopup(_daoCredentials, _viewModel));
        }


        public void OnAddClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(WebsiteEntry.Text) &&
                !string.IsNullOrEmpty(UserEntry.Text) &&
                !string.IsNullOrEmpty(PasswordEntry.Text)) // Ajout de cette condition
            {
                var credential = new TP2_SecurSimon.Models.Credentials
                {
                    //Id = Guid.NewGuid().ToString(),
                    Website = WebsiteEntry.Text,
                    User = UserEntry.Text,
                    Password = PasswordEntry.Text  // Sauvegardez le mot de passe ici
                };

                _daoCredentials.AddCredential(credential);
                PopupNavigation.Instance.PopAsync(true);
            }
        }
        public void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }


    }

}