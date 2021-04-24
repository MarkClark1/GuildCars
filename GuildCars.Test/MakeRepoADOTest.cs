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
    public class MakeRepoADOTest
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
        public void CanCreateMake()
        {
            MakeRepositoryADO repo = new MakeRepositoryADO();
            List<MakeViewModel> makesBefore = repo.GetAll().ToList();

            Make make = new Make()
            {
                Name = "TestMake",
                UserId = "00000000-0000-0000-0000-000000000000"
            };
            repo.Create(make);

            List<MakeViewModel> makesAfter = repo.GetAll().ToList();
            Assert.AreEqual(makesBefore.Count + 1, makesAfter.Count);
        }


        [Test]
        public void CanFailCreateMake()
        {
            MakeRepositoryADO repo = new MakeRepositoryADO();
            List<MakeViewModel> makesBefore = repo.GetAll().ToList();

            Make make = new Make()
            {
                Name = "TestMake",
                UserId = "00000000-0000-0000-0000-000000001000"
            };
            repo.Create(make);

            List<MakeViewModel> makesAfter = repo.GetAll().ToList();
            Assert.AreEqual(makesBefore.Count, makesAfter.Count);
        }

        [Test]
        public void CanFail2CreateMake()
        {
            MakeRepositoryADO repo = new MakeRepositoryADO();
            List<MakeViewModel> makesBefore = repo.GetAll().ToList();

            Make make = new Make()
            {
                Name = "",
                UserId = "00000000-0000-0000-0000-000000000000"
            };
            repo.Create(make);

            List<MakeViewModel> makesAfter = repo.GetAll().ToList();
            Assert.AreEqual(makesBefore.Count, makesAfter.Count);
        }

        [Test]
        public void CanFail3CreateMake()
        {
            MakeRepositoryADO repo = new MakeRepositoryADO();
            List<MakeViewModel> makesBefore = repo.GetAll().ToList();

            Make make = new Make()
            {
                Name = "TestMake",
                UserId = ""
            };
            repo.Create(make);

            List<MakeViewModel> makesAfter = repo.GetAll().ToList();
            Assert.AreEqual(makesBefore.Count, makesAfter.Count);
        }

        [Test]
        public void CanGetAllMakes()
        {
            MakeRepositoryADO repo = new MakeRepositoryADO();

            List<MakeViewModel> makes = repo.GetAll().ToList();

            Assert.AreEqual("Subaru", makes[0].Name);
            Assert.AreEqual("Ford", makes[1].Name);
        }
    }
}
