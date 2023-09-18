using Rg.Plugins.Popup.Services;
using System;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2_SecurSimon.Services; // Assurez-vous que le chemin est correct

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP2_SecurSimon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCredentialPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private Dao_Credentials _daoCredentials;

        public AddCredentialPopup(Dao_Credentials daoCredentials)
        {
            InitializeComponent();
            _daoCredentials = daoCredentials;
        }

        public void OnAddClicked(object sender, EventArgs e)
        {
            //je verifie que ce soit pas null ou vide
            if (!string.IsNullOrEmpty(WebsiteEntry.Text) && !string.IsNullOrEmpty(UserEntry.Text))
            {
                //nouvelle instance de classe 
                var credential = new TP2_SecurSimon.Models.Credentials
                {
                    //cree un ID unique global aleatoire 
                    Id = Guid.NewGuid().ToString(),
                    Website = WebsiteEntry.Text,
                    User = UserEntry.Text
                };

                _daoCredentials.AddCredential(credential);
                PopupNavigation.Instance.PopAsync(true);
            }
        }
    }

}