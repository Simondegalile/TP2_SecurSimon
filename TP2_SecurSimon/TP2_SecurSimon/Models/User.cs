using System;
using System.Collections.Generic;
using System.Text;

namespace TP2_SecurSimon.Models
{
    public class User
    {
        public int id { get; set; }
        public string user { get; set; }
        public string email { get; set; }
        public string Cryptepassword { get; set; }
        public string DateCreatUser { get; set; }
    }
}
