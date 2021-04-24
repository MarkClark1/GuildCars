using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories
{
    public class ContactRecordRepositoryADO : IContactRecordRepository
    {
        private string _connection = Settings.GetConnectionString();

        public ContactRecord Create(ContactRecord record)
        {
            if((string.IsNullOrEmpty(record.Email) && string.IsNullOrEmpty(record.Phone))
                || string.IsNullOrEmpty(record.Name) || string.IsNullOrEmpty(record.Message))
            {
                return null;
            } 

            string email = record.Email ?? "";
            string phone = record.Phone ?? "";
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = _connection;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "CreateContactRecord";
                    cmd.Parameters.AddWithValue("@Name", record.Name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Message", record.Message);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return record;
            }
            catch
            {
                return null;
            }
          
        }
    }
}
