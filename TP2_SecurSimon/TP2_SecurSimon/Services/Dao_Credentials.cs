using System;
using System.Collections.Generic;
using TP2_SecurSimon.Models;

namespace TP2_SecurSimon.Services
{
    public class Dao_Credentials
    {
        public List<Credentials> credentialsList;
        public Dao_Credentials() 
        {
            credentialsList = new List<Credentials>()
            {
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "Instagram", User="Katarine", Password="1234" },
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "X", User="Simon74X", Password="1234" },
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "RiotGames", User="Galiléo", Password="1234" },
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "SnapChat", User="sim-sou", Password="1234" },
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "Airbnb", User="Simon Almeida", Password="1234" },
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "EcoleDirect", User="Simon Dasilva", Password="1234" }
            };
        }
        // Pour obtenir tous les credentials
        public List<Credentials> GetAllCredentials()
        {
            return credentialsList;
        }


        public void AddCredential(Credentials credential)
        {
            credentialsList.Add(credential);
        }


    }
}
