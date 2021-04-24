using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.MockRepo
{
    class ContactRecordRepoMock : IContactRecordRepository
    {
        private static List<ContactRecord> _records = new List<ContactRecord>()
        {

        };
        public ContactRecord Create(ContactRecord record)
        {
            record.ContactRecordId = GetNextId();
            _records.Add(record);
            return record;
        }
        private int GetNextId()
        {
            if (_records[0] != null)
                return _records.Max(x => x.ContactRecordId) + 1;
            else
                return 1;
            
        }
    }
}
