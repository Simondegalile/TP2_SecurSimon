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

namespace TP2_SecurSimon.Views
{
    public partial class ItemsPage : ContentPage
    {
        Dao_Credentials _daoCredentials = new Dao_Credentials();
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
            BindingContext = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            //instance de classe 
            var popup = new AddCredentialPopup(_daoCredentials);
            //ajout de la fenetre de test "popup" à la pile de fenetre contextuelle gere par PopupNavigation
            await PopupNavigation.Instance.PushAsync(popup);
        }


    }
}