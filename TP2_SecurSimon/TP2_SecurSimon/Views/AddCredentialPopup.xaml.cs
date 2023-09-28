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

        public async void OnAddClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(WebsiteEntry.Text) &&
                !string.IsNullOrEmpty(UserEntry.Text) &&
                !string.IsNullOrEmpty(PasswordEntry.Text) &&
                !string.IsNullOrEmpty(EmailEntry.Text) &&
                !string.IsNullOrEmpty(CategoryEntry.Text))
            {
                var credential = new TP2_SecurSimon.Models.Credentials
                {
                    Website = WebsiteEntry.Text,
                    User = UserEntry.Text,
                    Password = PasswordEntry.Text,
                    Rating = GetRating(PasswordEntry.Text),
                    Email = EmailEntry.Text,
                    DateCreatPassword = DateTime.Now,
                    Categorie = CategoryEntry.Text,
                    IdUser = 1  
                };

                await _daoCredentials.AddCredentialAsync(credential);
                PopupNavigation.Instance.PopAsync(true);
            }
        }
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            // Handle the cancel button click event here.
            await PopupNavigation.Instance.PopAsync(true);
        }

        // Password strength calculator
        public string GetRating(string password)
        {
            if (password.Length < 6)
                return "Weak";
            else if (password.Length < 10)
                return "Medium";
            else
                return "Strong";
        }
    }
}
