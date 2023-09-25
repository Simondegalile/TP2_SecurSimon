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
        Dao_Credentials _daoCredentials = new Dao_Credentials();
        ItemsViewModel _viewModel;


        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddCredentialPopup(_daoCredentials));
        }
        private async void OnFrameTapped(object sender, EventArgs e)
        {
            var daoCredentials = new Dao_Credentials();
            await PopupNavigation.Instance.PushAsync(new AddCredentialPopup(daoCredentials));
        }

    }
}
