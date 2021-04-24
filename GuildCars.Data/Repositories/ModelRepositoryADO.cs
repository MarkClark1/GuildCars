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
    public class ModelRepositoryADO : IModelRepository
    {
        private string _connection = Settings.GetConnectionString();

        public Model Create(Model model)
        {
            if(model.Name == "" || model.Name == null)
            {
                return null;
            }
            if (model.UserId == "" || model.UserId == null)
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

                    SqlParameter param = new SqlParameter("@ModelId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.CommandText = "CreateModel";
                    cmd.Parameters.Add(param);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@MakeId", model.MakeId);
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    model.ModelId = (int)param.Value;
                }
                return model;
            }
            catch
            {
                return new Model();
            }
            
        }

        public IEnumerable<ModelViewModel> GetAll()
        {
            List<ModelViewModel> models = new List<ModelViewModel>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetAllModels";
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ModelViewModel current = new ModelViewModel();

                        current.ModelId = (int)dr["ModelId"];
                        current.ModelName = dr["ModelName"].ToString();
                        current.MakeName = dr["MakeName"].ToString();
                        current.DateAdded = (DateTime)dr["DateAdded"];
                        current.EmailOfAdder = dr["EmailOfAdder"].ToString();

                        models.Add(current);
                    }
                }
            }
            return models;
        }
    }
}
