using Template.DataAccess;
using Template.Domain.Data;
using Template.Domain.Entities;
using MongoDB.Bson;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Template.DataAccess.Tests
{

    [TestFixture]
    public class MongoTests
    {
        private Guid _firmId;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            _firmId = new Guid("3c59fb8b-1801-4961-95bd-c0e971ed625f");
        }

        [Test]
        public void CanGetData()
        {
            var dataContext = DataContextFatory.GetFirmDataContext<IFirmDataContext>();
            dataContext.Init(_firmId);

            var data = dataContext.ClientRepository.All();

            Assert.IsNotNull(data);
            Assert.Greater(data.Count, 0);

        }

        [Test]
        public void CanGetData_ByID()
        {
            var dataContext = DataContextFatory.GetFirmDataContext<IFirmDataContext>();
            dataContext.Init(_firmId);

            var data = dataContext.ClientRepository["5b11c2608472184d0e6f417c"];

            Assert.AreEqual("Law in ya Maw inc", data.ClientName);

        }

        [Test]
        public void CanInsertOne()
        {
            var dataContext = DataContextFatory.GetFirmDataContext<IFirmDataContext>();
            dataContext.Init(_firmId);

            var client = new Client();
            client.Id = ObjectId.GenerateNewId().ToString();
            client.ClientName = "This is a test";
            client.AmountPaid = 5.0M;

            dataContext.ClientRepository.Add(client);

            dataContext.Submit();

            dataContext.Cancel();

            var data = dataContext.ClientRepository[client.Id];

            Assert.AreEqual(client.Id, data.Id);
        }

        [Test]
        public void CanUpdateOne()
        {
            var dataContext = DataContextFatory.GetFirmDataContext<IFirmDataContext>();
            dataContext.Init(_firmId);

            var client = dataContext.ClientRepository.All().First();
            var originalAmountPaid = client.AmountPaid;
            client.AmountPaid = originalAmountPaid + 100;

            dataContext.Submit();

            dataContext.Cancel();

            var data = dataContext.ClientRepository.All().First();

            Assert.AreEqual(originalAmountPaid + 100, data.AmountPaid);

        }

        [Test]
        public void CanGetSubCollection()
        {
            var dataContext = DataContextFatory.GetFirmDataContext<IFirmDataContext>();
            dataContext.Init(_firmId);

            var data = dataContext.ClientRepository.All().First();

            Assert.IsNotNull(data.Addresses);
            Assert.AreEqual(2, data.Addresses.Count());
        }

        [Test]
        public void CanInsertNewSubCollection_OnExistingEntity()
        {
            var dataContext = DataContextFatory.GetFirmDataContext<IFirmDataContext>();
            dataContext.Init(_firmId);

            var client = dataContext.ClientRepository.All().Last();
            client.Addresses = new List<Address>
            {
                new Address
                {
                    FirstLine = "test",
                    SecondLine = "test2",
                    Postcode = "poe23dsfs"
                }
            };

            dataContext.Submit();

            dataContext.Cancel();

            var data = dataContext.ClientRepository.All().Last();

            Assert.IsNotNull(data.Addresses);
            Assert.AreEqual(1, data.Addresses.Count());
        }

        [Test]
        public void CanUpdateSubCollection()
        {
            var dataContext = DataContextFatory.GetFirmDataContext<IFirmDataContext>();
            dataContext.Init(_firmId);

            var client = dataContext.ClientRepository.All().Last();
            client.Addresses.First().FirstLine = "This has fucking changed";
            dataContext.Submit();

            dataContext.Cancel();

            var data = dataContext.ClientRepository.All().Last();

            Assert.IsNotNull(data.Addresses);
            Assert.AreEqual("This has fucking changed", data.Addresses.First().FirstLine);
        }
    }
}
