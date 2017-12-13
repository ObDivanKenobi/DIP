using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Models
{
    class PasswordHasher
    {
        public string GetHash(string password)
        {
            return password.GetHashCode().ToString();
        }
    }
}
