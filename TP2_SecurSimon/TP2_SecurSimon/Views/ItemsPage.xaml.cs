// Importation des bibliothèques nécessaires
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2_SecurSimon.Models;
using TP2_SecurSimon.ViewModels;
using TP2_SecurSimon.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TP2_SecurSimon.Services;
using Rg.Plugins.Popup.Extensions;

namespace TP2_SecurSimon.Views
{
    public partial class ItemsPage : ContentPage
    {
        // Déclaration et initialisation de la variable pour accéder aux méthodes de Dao_Credentials
        Dao_Credentials _daoCredentials = new Dao_Credentials();
        // Déclaration de la variable pour le ViewModel associé à cette page
        ItemsViewModel _viewModel;

        // Constructeur de la page
        public ItemsPage()
        {
            InitializeComponent(); // Initialisation des composants de la vue

            // Liaison du ViewModel à cette page
            BindingContext = _viewModel = new ItemsViewModel();
        }

        // Méthode appelée lorsque la page est affichée
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing(); // Appel de la méthode OnAppearing du ViewModel
        }

        // Gestionnaire d'événements pour le clic sur le bouton d'ajout d'élément
        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddCredentialPopup(_daoCredentials));
        }

        // Gestionnaire d'événements pour le tap sur un élément (Frame)
        private async void OnFrameTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame; // Conversion de l'expéditeur en Frame
            if (frame != null)
            {
                var selectedCredential = frame.BindingContext as Credentials; // Récupération de l'élément associé
                if (selectedCredential != null)
                {
                    // Affichage de la popup d'édition pour l'élément sélectionné
                    await PopupNavigation.Instance.PushAsync(new EditCredentialPopup(selectedCredential));
                }
                else
                {
                    // Si aucun élément n'est associé, aucune action n'est effectuée
                }
            }
        }

        // Gestionnaire d'événements pour le clic sur le bouton de suppression
        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button; // Conversion de l'expéditeur en Button
            if (button != null)
            {
                var selectedCredential = button.BindingContext as Credentials; // Récupération de l'élément associé
                if (selectedCredential != null)
                {
                    // Demande de confirmation à l'utilisateur avant la suppression
                    var confirm = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer cette entrée?", "Oui", "Non");
                    if (confirm)
                    {
                        // Si l'utilisateur confirme, utilisation de la commande de suppression du ViewModel
                        _viewModel.DeleteCommand.Execute(selectedCredential.Id);
                    }
                }
            }
        }
    }
}
