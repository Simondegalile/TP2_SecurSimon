using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TP2_SecurSimon.Models;
using TP2_SecurSimon.Services;
using TP2_SecurSimon.Views;
using TP2_SecurSimon.Models;
using Xamarin.Forms;

namespace TP2_SecurSimon.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        // Initialisation du DAO (Data Access Object) pour interagir avec la base de données utilisateur
        private UserDao _userDao;

        // Propriétés pour stocker le nom d'utilisateur et le mot de passe entrés par l'utilisateur
        public string User { get; set; }
        public string Password { get; set; }

        // Valeurs de nom d'utilisateur et de mot de passe de connexion pré-définies
        public string Password_connexion = "1234";
        public string User_connexion = "test";

        // Commande qui sera déclenchée lorsque l'utilisateur appuie sur le bouton de connexion
        public Command LoginCommand { get; }

        // Constructeur du ViewModel
        public LoginViewModel()
        {
            // Initialisation du DAO utilisateur avec le chemin "Service/Dao_User"
            _userDao = new UserDao("Service/Dao_User");

            // Création de la commande de connexion qui appelle la méthode OnLoginClicked
            LoginCommand = new Command(OnLoginClicked);
        }


        // Méthode exécutée lorsque l'utilisateur appuie sur le bouton de connexion
        private async void OnLoginClicked(object obj)
        {
            // Récupération du nom d'utilisateur et du mot de passe entrés par l'utilisateur
            string UserConnexion = User;
            string EncryptedPasswordConnexion = Cryptage.Encrypt(Password); // Encrypt user's entered password

            // Recherche d'un utilisateur dans la base de données en fonction de l'adresse e-mail entrée
            User user = _userDao.GetUserByEmail(UserConnexion);

            if (user != null)
            {
                // Compare encrypted passwords instead of plain text
                if (EncryptedPasswordConnexion == user.Cryptepassword)
                {
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Échec de la connexion", "Le mot de passe que vous avez entré est incorrect. Veuillez vérifier votre saisie et réessayer.", "OK");
                }
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("Échec de la connexion", "Utilisateur non trouvé.", "OK");
            }
        }





        //if(User == User_connexion && Password == Password_connexion)
        //{
        //    // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
        //    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        //}                         
        //else
        //{
        //    await App.Current.MainPage.DisplayAlert("Échec de la connexion", "Le mot de passe que vous avez entré est incorrect. Veuillez vérifier votre saisie et réessayer.", "OK");
        //}
                                                               
    }
                                                                                                                  
}
