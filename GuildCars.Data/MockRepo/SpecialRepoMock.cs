using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;

namespace GuildCars.Data.MockRepo
{
    public class SpecialRepoMock : ISpecialRepository
    {
        private static List<Special> _specials = new List<Special>()
        {
            new Special(){SpecialId=1,Description="Its a special n sutff",Title="The first special"},
            new Special(){SpecialId=2,Description="Its the next special n stuff",Title="THe second special"}
        };

        public Special Create(Special special)
        {
            special.SpecialId = GetNextId();
            _specials.Add(special);
            return special;
        }

        public void Delete(int id)
        {
            _specials.RemoveAll(x => x.SpecialId == id);
        }

        public IEnumerable<Special> GetAll()
        {
            return _specials;
        }

        private int GetNextId()
        {
            return _specials.Max(x => x.SpecialId) + 1;
        }
    }
}
