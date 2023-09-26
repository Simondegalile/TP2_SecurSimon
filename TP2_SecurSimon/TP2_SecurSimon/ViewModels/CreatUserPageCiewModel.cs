using System;
using System.Threading.Tasks;
using TP2_SecurSimon.Models;
using TP2_SecurSimon.Views;
using Xamarin.Forms;

namespace TP2_SecurSimon.ViewModels
{
    public class CreatUserPageViewModel : BaseViewModel
    {
        // Initialisation du DAO (Data Access Object) pour interagir avec la base de données utilisateur
        private UserDao _userDao;

        // Propriétés pour stocker le nom d'utilisateur, le mot de passe, et l'email entrés par l'utilisateur
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Commande qui sera déclenchée lorsque l'utilisateur appuie sur le bouton de création de compte
        public Command CreateUserCommand { get; }

        // Constructeur du ViewModel
        public CreatUserPageViewModel()
        {
            // Initialisation du DAO utilisateur avec le chemin "Service/Dao_User"
            _userDao = new UserDao("Service/Dao_User");

            // Création de la commande de création de compte qui appelle la méthode OnCreateUserClicked
            CreateUserCommand = new Command(async () => await OnCreateUserClicked());
        }









        private async Task OnCreateUserClicked()
        {
            var newUser = new User
            {
                name = Name,
                email = Email,
                Cryptepassword = Cryptage.Encrypt(Password) // Encrypt user's entered password
            };

            bool success = await _userDao.AddUser(newUser);

            if (success)
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Création du compte", "Échec de la création du compte. Veuillez réessayer.", "OK");
            }
        }
    }
}
