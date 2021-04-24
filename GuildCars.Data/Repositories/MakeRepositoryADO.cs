using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
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
    public class MakeRepositoryADO : IMakeRepository
    {
        private string _connection = Settings.GetConnectionString();

        public Make Create(Make make)
        {
            if (make.Name == null || make.Name == "")
            {
                return null;
            }
            if (make.UserId == null || make.UserId == "")
            {
                return null;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = _connection;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@MakeId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.CommandText = "CreateMake";
                    cmd.Parameters.Add(param);
                    cmd.Parameters.AddWithValue("@Name", make.Name);
                    cmd.Parameters.AddWithValue("@UserId", make.UserId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    make.MakeId = (int)param.Value;
                }
                return make;
            }
            catch
            {
                return null;
            }
           
        }

        public IEnumerable<MakeViewModel> GetAll()
        {
            List<MakeViewModel> makes = new List<MakeViewModel>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetAllMakes";
                cmd.CommandType = CommandType.StoredProcedure;          

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MakeViewModel current = new MakeViewModel();

                        current.MakeId = (int)dr["MakeId"];
                        current.Name = dr["MakeName"].ToString();
                        current.DateAdded = (DateTime)dr["DateAdded"];
                        current.EmailOfAdder = dr["EmailOfAdder"].ToString();

                        makes.Add(current);
                    }
                }
            }
            return makes;
        }
    }
}
