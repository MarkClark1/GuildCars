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
    public class SpecialRepositoryADO : ISpecialRepository
    {
        private string _connection = Settings.GetConnectionString();

        public Special Create(Special special)
        {
            if(string.IsNullOrEmpty(special.Description) || string.IsNullOrEmpty(special.Title))
            {
                return null;
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@SpecialId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.CommandText = "CreateSpecial";
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@Title", special.Title);
                cmd.Parameters.AddWithValue("@Description", special.Description);

                conn.Open();
                cmd.ExecuteNonQuery();
                special.SpecialId = (int)param.Value;
            }
            return special;
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "DeleteSpecial";
                cmd.Parameters.AddWithValue("@Id", id);
   
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Special> GetAll()
        {
            List<Special> specials = new List<Special>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetAllSpecials";
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Special current = new Special();

                        current.SpecialId= (int)dr["SpecialId"];
                        current.Title = dr["Title"].ToString();
                        current.Description = dr["Description"].ToString();

                        specials.Add(current);
                    }
                }
            }
            return specials;
        }
    }
}
