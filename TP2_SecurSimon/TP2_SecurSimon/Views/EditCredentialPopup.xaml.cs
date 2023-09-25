using System;
using TP2_SecurSimon.Models;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP2_SecurSimon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCredentialPopup : PopupPage
    {
        public Credentials CurrentCredentials { get; set; }

        public EditCredentialPopup(Credentials credentials)
        {
            InitializeComponent();

            CurrentCredentials = credentials;
            BindingContext = CurrentCredentials;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            // Your logic to save the modifications (e.g., update in database) here.
            // ...

            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
    }
}
