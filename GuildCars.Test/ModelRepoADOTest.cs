using GuildCars.Data.Repositories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Test
{
    [TestFixture]
    public class ModelRepoADOTest
    {
        [TearDown]
        public static void TearDownDatabase()
        {
            using (var cn = new SqlConnection("Server = localhost; Database = GuildCars; Integrated Security = True;"))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "TearDownTestData";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
        [SetUp]
        public static void SetUpTestDatabase()
        {
            using (var cn = new SqlConnection("Server = localhost; Database = GuildCars; Integrated Security = True;"))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "RestTestData";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanCreateModel()
        {
            ModelRepositoryADO repo = new ModelRepositoryADO();
            List<ModelViewModel> modelsBefore = repo.GetAll().ToList();

            Model model = new Model()
            {
                Name = "TestModel",
                MakeId = 1,
                UserId = "00000000-0000-0000-0000-000000000000"
            };
            var result = repo.Create(model);

            List<ModelViewModel> modelsAfter = repo.GetAll().ToList();
            Assert.AreEqual(modelsBefore.Count + 1, modelsAfter.Count);
            Assert.AreEqual(1, result.MakeId);
        }

        [Test]
        public void CanFailCreateModel()
        {
            ModelRepositoryADO repo = new ModelRepositoryADO();
            List<ModelViewModel> modelsBefore = repo.GetAll().ToList();

            Model model = new Model()
            {
                Name = "TestModel",
                MakeId = 1001,
                UserId = "00000000-0000-0000-0000-000000000000"
            };
            var result = repo.Create(model);

            List<ModelViewModel> modelsAfter = repo.GetAll().ToList();
            Assert.AreEqual(modelsBefore.Count, modelsAfter.Count);
        }

        [Test]
        public void CanFailCreate2Model()
        {
            ModelRepositoryADO repo = new ModelRepositoryADO();
            List<ModelViewModel> modelsBefore = repo.GetAll().ToList();

            Model model = new Model()
            {
                Name = "TestModel",
                MakeId = 1,
                UserId = "00000000-0000-0000-0000-000002000000"
            };
            var result = repo.Create(model);

            List<ModelViewModel> modelsAfter = repo.GetAll().ToList();
            Assert.AreEqual(modelsBefore.Count, modelsAfter.Count);
        }

        [Test]
        public void CanFailCreate3Model()
        {
            ModelRepositoryADO repo = new ModelRepositoryADO();
            List<ModelViewModel> modelsBefore = repo.GetAll().ToList();

            Model model = new Model()
            {
                Name = "",
                MakeId = 1,
                UserId = "00000000-0000-0000-0000-000000000000"
            };
            var result = repo.Create(model);

            List<ModelViewModel> modelsAfter = repo.GetAll().ToList();
            Assert.AreEqual(modelsBefore.Count, modelsAfter.Count);
        }

        [Test]
        public void CanFailCreate4Model()
        {
            ModelRepositoryADO repo = new ModelRepositoryADO();
            List<ModelViewModel> modelsBefore = repo.GetAll().ToList();

            Model model = new Model()
            {
                Name = "TestModel",
                MakeId = 1,
                UserId = ""
            };
            var result = repo.Create(model);

            List<ModelViewModel> modelsAfter = repo.GetAll().ToList();
            Assert.AreEqual(modelsBefore.Count, modelsAfter.Count);
        }

        [Test]
        public void CanGetAllModels()
        {
            ModelRepositoryADO repo = new ModelRepositoryADO();

            List<ModelViewModel> models = repo.GetAll().ToList();

            Assert.AreEqual("Subaru", models[0].MakeName);
            Assert.AreEqual("Impreza", models[0].ModelName);
            Assert.AreEqual("Ford", models[1].MakeName);
        }
    }
}
