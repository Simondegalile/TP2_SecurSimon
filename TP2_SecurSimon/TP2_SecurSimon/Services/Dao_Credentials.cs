using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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
                new Credentials { Id = 1, Website = "Instagram", User="Katarine", Password="1234" },
                new Credentials { Id = 2, Website = "X", User="Simon74X", Password="1234" },
                new Credentials { Id = 3, Website = "RiotGames", User="Galiléo", Password="1234" },
                new Credentials { Id = 4, Website = "SnapChat", User="sim-sou", Password="1234" },
                new Credentials { Id = 5, Website = "Airbnb", User="Simon Almeida", Password="1234" },
                new Credentials { Id = 6, Website = "EcoleDirect", User="Simon Dasilva", Password="1234" }
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
