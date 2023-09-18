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
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "blabla item", User="This is an item description." },
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "Second item", User="This is an item description." },
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "Third item", User="This is an item description." },
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "Fourth item", User="This is an item description." },
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "Fifth item", User="This is an item description." },
                new Credentials { Id = Guid.NewGuid().ToString(), Website = "Sixth item", User="This is an item description." }
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
