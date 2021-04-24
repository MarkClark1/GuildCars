using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.MockRepo
{
    public class MakeRepoMock : IMakeRepository
    {
        private static List<Make> _makes = new List<Make>()
        {
            new Make(){ Name="Subaru",MakeId=1,UserId="00000000-0000-0000-0000-000000000000",DateAdded = new DateTime(2019,5,5)},
            new Make(){Name="Ford",MakeId=2,UserId="00000000-0000-0000-0000-000000000000",DateAdded = new DateTime(2018,5,5)}
        };

        public Make Create(Make make)
        {
            make.MakeId = GetNextId();
            _makes.Add(make);
            return make;
        }

        public IEnumerable<MakeViewModel> GetAll()
        {
            //TODO: I am just going o add a made up email address that isnt attached to a user.. maybe fix this?
            List<MakeViewModel> returnList = new List<MakeViewModel>();

            foreach(var m in _makes)
            {
                MakeViewModel make = new MakeViewModel()
                {
                    MakeId = m.MakeId,
                    DateAdded = m.DateAdded,
                    EmailOfAdder = "TestEmail@email.com",
                    Name = m.Name
                };

                returnList.Add(make);
            }

            return returnList;
        }

        private int GetNextId()
        {
            return _makes.Max(x => x.MakeId) + 1;
        }
    }
}
