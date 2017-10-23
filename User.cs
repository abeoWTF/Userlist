using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Labb5
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        
        public User() { }

        public User(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        

        //public override string ToString()
        //{

        //    string newFormat = string.Format("[{0}, {1}]", Name, Email);
        //    return newFormat;

        //}


    }
}
