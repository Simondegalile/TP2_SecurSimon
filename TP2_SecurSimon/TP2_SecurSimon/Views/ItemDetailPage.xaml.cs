using Rg.Plugins.Popup.Services;
using System;
using System.ComponentModel;
using TP2_SecurSimon.Services;
using TP2_SecurSimon.ViewModels;
using Xamarin.Forms;

namespace TP2_SecurSimon.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }


    }
}