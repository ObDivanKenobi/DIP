using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOk.Models
{
    public interface IStorage<TUser> where TUser : User
    {
        QueryResult Save(TUser user);
        TUser FindByLogin(string username, string passwordHash);
    }
}
