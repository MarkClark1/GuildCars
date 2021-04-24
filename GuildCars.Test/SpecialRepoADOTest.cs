using GuildCars.Data.Repositories;
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
    public class SpecialRepoADOTest
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
        public void CanCreateAndDeleteSpecial()
        {
            SpecialRepositoryADO repo = new SpecialRepositoryADO();

            Special special = new Special()
            {
                Description = "Heres a Special",
                Title = "Test Title"
            };

            var specialsBefore = repo.GetAll();
            var returnSpecial = repo.Create(special);
            var specialsAfter = repo.GetAll();

            Assert.AreEqual(specialsBefore.Count() + 1, specialsAfter.Count());

            specialsBefore = repo.GetAll();
            repo.Delete(returnSpecial.SpecialId);
            specialsAfter = repo.GetAll();

            Assert.AreEqual(specialsBefore.Count() - 1, specialsAfter.Count());
        }

        [Test]
        public void CanCreateFailCreate1()
        {
            SpecialRepositoryADO repo = new SpecialRepositoryADO();

            Special special = new Special()
            {
                Description = "Heres a Special"
            };

            var returnSpecial = repo.Create(special);
            Assert.AreEqual(null, returnSpecial);
        }


        [Test]
        public void CanCreateFailCreate2()
        {
            SpecialRepositoryADO repo = new SpecialRepositoryADO();

            Special special = new Special()
            {
                Title = "Test Title"
            };

            var returnSpecial = repo.Create(special);
            Assert.AreEqual(null, returnSpecial);
        }

        [Test]
        public void CanCreateFailCreate3()
        {
            SpecialRepositoryADO repo = new SpecialRepositoryADO();

            Special special = new Special()
            {
                Description = "",
                Title = "Test Title"
            };

            var returnSpecial = repo.Create(special);
            Assert.AreEqual(null, returnSpecial);
        }

        [Test]
        public void CanCreateFailCreate4()
        {
            SpecialRepositoryADO repo = new SpecialRepositoryADO();

            Special special = new Special()
            {
                Description = "Heres a Special",
                Title = ""
            };

            var returnSpecial = repo.Create(special);
            Assert.AreEqual(null, returnSpecial);
        }

    }
}
