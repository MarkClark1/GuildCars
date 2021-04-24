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
    public class UserRepositoryADO : IUserRepository
    {
        private string _connection = Settings.GetConnectionString();
        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetUsers";
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                            User current = new User();

                        current.FirstName = dr["FirstName"].ToString();
                        current.LastName = dr["LastName"].ToString();
                        current.Email = dr["Email"].ToString();
                        current.Role = dr["Role"].ToString();
                        current.Id = dr["Id"].ToString();

                        users.Add(current);
                    }
                }
            }
            return users;
        }

        public User GetUserById(string id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetUserById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        User current = new User();

                        current.FirstName = dr["FirstName"].ToString();
                        current.LastName = dr["LastName"].ToString();
                        current.Email = dr["Email"].ToString();
                        current.Role = dr["Role"].ToString();
                        current.Id = dr["Id"].ToString();

                        return current;
                    }
                }
            }
            return null;
        }

        public void UpDateFirstLastName(string lastName, string FirstName, string id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UpdateFirstLastName";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);

                conn.Open();
                cmd.ExecuteNonQuery();        
            }
        }
    }
}
