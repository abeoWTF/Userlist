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
        public string FullName { get; set; }
        public string Email { get; set; }
        
       

        public User(string name, string email)
        {
            this.FullName = name;
            this.Email = email;
        }



        public override string ToString()
        {

            return String.Format("Email: {0}", this.Email);

        }


    }
}
