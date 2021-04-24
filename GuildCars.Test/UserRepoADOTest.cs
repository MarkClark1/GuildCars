using GuildCars.Data.Repositories;
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
    public class UserRepoADOTest
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
        public static void CanGetUser()
        {
            UserRepositoryADO repo = new UserRepositoryADO();
            var result = repo.GetUserById("00000000-0000-0000-0000-000000000000");
            Assert.AreEqual("John", result.FirstName);
            Assert.AreEqual("smith", result.LastName);
            Assert.AreEqual("test@test.com", result.Email);
        }


        [Test]
        public static void CanGetUsers()
        {
            UserRepositoryADO repo = new UserRepositoryADO();
            var result = repo.GetAll().ToList();
            Assert.GreaterOrEqual(result.Count,1);
        }


        [Test]
        public static void UpdateFirstLastName()
        {
            UserRepositoryADO repo = new UserRepositoryADO();
            var before = repo.GetUserById("00000000-0000-0000-0000-000000000000");
            Assert.AreEqual("John", before.FirstName);
            Assert.AreEqual("smith", before.LastName);
            Assert.AreEqual("test@test.com", before.Email);
            repo.UpDateFirstLastName("Lee","Bob", "00000000-0000-0000-0000-000000000000");
            var after = repo.GetUserById("00000000-0000-0000-0000-000000000000");
            Assert.AreEqual("Bob", after.FirstName);
            Assert.AreEqual("Lee", after.LastName);
            Assert.AreEqual("test@test.com", after.Email);
        }
    }
}
