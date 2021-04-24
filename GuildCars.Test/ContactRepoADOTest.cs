using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
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
    public class ContactRepoADOTest
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
        public void CanCreate1Contact()
        {
            IContactRecordRepository repo = RepoFactory.CreateContactRecordRepo();
            ContactRecord record = new ContactRecord()
            {
                Email = "Email@Test.com",
                Name = "TestContact",
                Message = "Blah blah",
                Phone = "701-701-7011"
            };           

            var result = repo.Create(record);

            Assert.GreaterOrEqual(1, result.ContactRecordId);
            Assert.AreEqual(record, result);
        }

        [Test]
        public void CanCreate2Contact()
        {
            IContactRecordRepository repo = RepoFactory.CreateContactRecordRepo();
            ContactRecord record = new ContactRecord()
            {
                Email = "",
                Name = "TestContact",
                Message = "Blah blah",
                Phone = "701-701-7011"
            };

            var result = repo.Create(record);

            Assert.GreaterOrEqual(1, result.ContactRecordId);
            Assert.AreEqual(record, result);
        }

        [Test]
        public void CanCreate3Contact()
        {
            IContactRecordRepository repo = RepoFactory.CreateContactRecordRepo();
            ContactRecord record = new ContactRecord()
            {
                Email = "email@Test.com",
                Name = "TestContact",
                Message = "Blah blah",
                Phone = ""
            };

            var result = repo.Create(record);

            Assert.GreaterOrEqual(1, result.ContactRecordId);
            Assert.AreEqual(record, result);
        }

        [Test]
        public void CanFail1CreateContact()
        {
            IContactRecordRepository repo = RepoFactory.CreateContactRecordRepo();
            ContactRecord record = new ContactRecord()
            {
                Email = "",
                Name = "TestContact",
                Message = "Blah blah",
                Phone = ""
            };

            var result = repo.Create(record);

            Assert.AreEqual(null, result);
        }


        [Test]
        public void CanFail2CreateContact()
        {
            IContactRecordRepository repo = RepoFactory.CreateContactRecordRepo();
            ContactRecord record = new ContactRecord()
            {
                Email = "email@test.com",
                Name = "",
                Message = "Blah blah",
                Phone = "701-701-7011"
            };

            var result = repo.Create(record);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFail3CreateContact()
        {
            IContactRecordRepository repo = RepoFactory.CreateContactRecordRepo();
            ContactRecord record = new ContactRecord()
            {
                Email = "email@test.com",
                Name = "TestContact",
                Message = "",
                Phone = "701-701-7011"
            };

            var result = repo.Create(record);

            Assert.AreEqual(null, result);
        }
    }
}
