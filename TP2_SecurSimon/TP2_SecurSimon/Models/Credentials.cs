using System;
using System.Collections.Generic;
using System.Text;

namespace TP2_SecurSimon.Models
{
    public class Credentials 
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string User { get; set; }
        public string Rating { get; set; }
        public string Website { get; set; }
        public string Email{ get; set; }
        public DateTime DateCreatPassword { get; set; }
        public string Categorie { get; set; }
        public int IdUser { get; set; }

    }
}
