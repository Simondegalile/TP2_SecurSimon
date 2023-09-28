using System;
using System.Windows.Input;
using TP2_SecurSimon.Models;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace TP2_SecurSimon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCredentialPopup : PopupPage
    {
       

        public EditCredentialPopup(Credentials credentials)
        {
            InitializeComponent();
            BindingContext = credentials; // Utilisez le paramètre 'credentials' pour la liaison de données
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
           await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
           await PopupNavigation.Instance.PopAsync();
        }
        public void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}
