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
                new Credentials { Id = Guid.NewGuid().ToString(), Password = "First item", User="This is an item description." },
                new Credentials { Id = Guid.NewGuid().ToString(), Password = "Second item", User="This is an item description." },
                new Credentials { Id = Guid.NewGuid().ToString(), Password = "Third item", User="This is an item description." },
                new Credentials { Id = Guid.NewGuid().ToString(), Password = "Fourth item", User="This is an item description." },
                new Credentials { Id = Guid.NewGuid().ToString(), Password = "Fifth item", User="This is an item description." },
                new Credentials { Id = Guid.NewGuid().ToString(), Password = "Sixth item", User="This is an item description." }
            };
        }

        // Pour obtenir tous les credentials
        public List<Credentials> GetAllCredentials()
        {
            return credentialsList;
        }
    }
}
