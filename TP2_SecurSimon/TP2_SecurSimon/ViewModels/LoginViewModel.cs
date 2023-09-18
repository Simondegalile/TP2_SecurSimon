using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TP2_SecurSimon.Views;
using Xamarin.Forms;

namespace TP2_SecurSimon.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string User { get; set; }
        public string Password { get; set; }


        public string Password_connexin = "1234";
        public string User_connexion = "test";


        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            if(User == User_connexion && Password == Password_connexin)
            {
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Échec de la connexion", "Le mot de passe que vous avez entré est incorrect. Veuillez vérifier votre saisie et réessayer.", "OK");
            }


        }
    }
}
