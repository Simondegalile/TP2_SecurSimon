using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TP2_SecurSimon.Models;
using TP2_SecurSimon.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP2_SecurSimon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatUserPage : ContentPage
    {
        
        public CreatUserPage()
        {
            InitializeComponent();
            BindingContext = new CreatUserPageViewModel();
        }



      
    }
}