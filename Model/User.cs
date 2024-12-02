using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Model
{
    public class User
    {
        public int UserId { get; set; } // Простое целое число для уникального ID
        public string Login { get; set; }
        public string Password { get; set; }
    }

}
