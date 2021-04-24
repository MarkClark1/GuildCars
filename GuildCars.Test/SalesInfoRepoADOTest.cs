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
    public class SalesInfoRepoADOTest
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
        public void CanCreateSale1()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                PurchaseDate = DateTime.Today,
                StreetOne = "Street one",
                StreetTwo = "Street two",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(sale, result);
        }

        [Test]
        public void CanCreateSale2()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(sale, result);
        }

        [Test]
        public void CanFailCreateSale1()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale2()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 123455,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale3()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 1234,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale4()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale5()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale6()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 0,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale7()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale8()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale10()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale11()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = new DateTime(),
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale12()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 300,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale13()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 0,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CanFailCreateSale14()
        {
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();

            SaleInfo sale = new SaleInfo()
            {
                City = "City",
                ZipCode = 12345,
                Name = "TestSale",
                Email = "email@test.com",
                PurchasePrice = 100M,
                Phone = "123-123-1234",
                State = "MN",
                StreetOne = "Street one",
                PurchaseDate = DateTime.Today,
                StreetTwo = "",
                PurchaseType = "Bank",
                UserId = "0000-000000-0000000-0000000",
                VehicleId = 0,
            };

            var result = repo.Create(sale);

            Assert.AreEqual(null, result);
        }
    }
}
