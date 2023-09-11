using System;
using System.Collections.Generic;
using System.ComponentModel;
using TP2_SecurSimon.Models;
using TP2_SecurSimon.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP2_SecurSimon.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}