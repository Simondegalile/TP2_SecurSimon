// Importation des bibliothèques nécessaires
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
    // Cette annotation indique que le fichier XAML associé doit être compilé
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCredentialPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        // Déclaration des variables privées pour stocker les instances de Dao_Credentials et ItemsViewModel
        private Dao_Credentials _daoCredentials;
        private ItemsViewModel _viewModel;

        // Constructeur pour initialiser la popup avec seulement Dao_Credentials
        public AddCredentialPopup(Dao_Credentials daoCredentials)
        {
            InitializeComponent(); // Initialisation des composants de la vue
            _daoCredentials = daoCredentials; // Affectation de la valeur à la variable privée
        }

        // Constructeur surchargé pour initialiser la popup avec Dao_Credentials et ItemsViewModel
        public AddCredentialPopup(Dao_Credentials daoCredentials, ItemsViewModel viewModel)
        {
            InitializeComponent(); // Initialisation des composants de la vue
            _daoCredentials = daoCredentials; // Affectation de la valeur à la variable privée
            _viewModel = viewModel; // Stockage du ViewModel passé en paramètre
        }

        // Gestionnaire d'événements pour le clic sur le bouton d'ajout d'élément
        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddCredentialPopup(_daoCredentials, _viewModel));
        }

        // Gestionnaire d'événements pour le clic sur le bouton d'ajout
        public async void OnAddClicked(object sender, EventArgs e)
        {
            // Vérification que tous les champs sont remplis
            if (!string.IsNullOrEmpty(WebsiteEntry.Text) &&
                !string.IsNullOrEmpty(UserEntry.Text) &&
                !string.IsNullOrEmpty(PasswordEntry.Text) &&
                !string.IsNullOrEmpty(EmailEntry.Text) &&
                !string.IsNullOrEmpty(CategoryEntry.Text))
            {
                // Création d'une nouvelle instance de Credentials avec les valeurs saisies
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

                // Ajout de la nouvelle credential à la base de données
                await _daoCredentials.AddCredentialAsync(credential);
                // Fermeture de la popup
                PopupNavigation.Instance.PopAsync(true);
            }
        }

        // Gestionnaire d'événements pour le clic sur le bouton d'annulation
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            // Fermeture de la popup
            await PopupNavigation.Instance.PopAsync(true);
        }

        // Méthode pour évaluer la force du mot de passe
        public string GetRating(string password)
        {
            if (password.Length < 6)
                return "Weak"; // Faible
            else if (password.Length < 10)
                return "Medium"; // Moyen
            else
                return "Strong"; // Fort
        }
    }
}
