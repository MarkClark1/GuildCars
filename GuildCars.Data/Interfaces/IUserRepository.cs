using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetUserById(string id);
        void UpDateFirstLastName(string lastName, string FirstName, string id);
    }
}
