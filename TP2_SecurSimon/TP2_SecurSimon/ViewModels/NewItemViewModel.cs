using System;
using System.Windows.Input;
using TP2_SecurSimon.Models;
using Xamarin.Forms;

namespace TP2_SecurSimon.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        // Propriétés privées pour stocker le texte et la description
        private string text;
        private string description;

        // Constructeur de la classe
        public NewItemViewModel()
        {
            // Initialisation des commandes
            SaveCommand = new Command(OnSave, ValidateSave); // Commande pour sauvegarder
            CancelCommand = new Command(OnCancel); // Commande pour annuler

            // Événement déclenché lorsqu'une propriété change
            // Il permet de vérifier si la commande Save peut être exécutée
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        // Méthode pour valider si les données sont correctes avant de sauvegarder
        private bool ValidateSave()
        {
            // Vérifie que les champs text et description ne sont pas vides
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        // Propriété pour le texte
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value); // Met à jour la valeur et déclenche l'événement PropertyChanged
        }

        // Propriété pour la description
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value); // Met à jour la valeur et déclenche l'événement PropertyChanged
        }

        // Commandes pour sauvegarder et annuler
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        // Méthode appelée lors de l'annulation de la création d'un nouvel élément
        private async void OnCancel()
        {
            // Cette commande ferme la page actuelle et retourne à la page précédente
            await Shell.Current.GoToAsync("..");
        }

        // Méthode appelée lors de la sauvegarde d'un nouvel élément
        private async void OnSave()
        {
            // Création d'un nouvel élément
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(), // Génération d'un nouvel identifiant unique
                Text = Text,
                Description = Description
            };

            // Ajout du nouvel élément à la base de données
            await DataStore.AddItemAsync(newItem);

            // Cette commande ferme la page actuelle et retourne à la page précédente
            await Shell.Current.GoToAsync("..");
        }
    }
}
