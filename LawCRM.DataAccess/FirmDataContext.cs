using Template.DataAccess.Repositories;
using Template.DataAccess.Storage;
using Template.Domain.Data;
using Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Conventions;
using Castle.DynamicProxy;
using Template.DataAccess.DataPOCOs;

namespace Template.DataAccess
{
    internal class FirmDataContext : MongoDataContext, IFirmDataContext, IDataContext
    {
        private Guid _firmId;
        private static ProxyGenerator _generator = new ProxyGenerator();

        public FirmDataContext()
        {
            var pack = new ConventionPack
            {
                new StringObjectIdConvention()
            };

            ConventionRegistry.Register("MongoConvention", pack, _ => true);
        }

        private IClientRepository _clientRepo;
        public IClientRepository ClientRepository
        {
            get
            {
                if (_clientRepo == null)
                {
                    SetClientData();
                }

                return _clientRepo;
            }
            set { _clientRepo = value; }
        }



        public void Init(Guid firmId)
        {
            _firmId = firmId;
        }

        public void Cancel()
        {
            SetClientData();
        }

        public void Submit()
        {
            var repo = (ClientRepository)_clientRepo;
            var collection = Database.GetCollection<Client>($"firm[{_firmId}]::client");

            DeleteEntities(collection, repo);
            UpdateEntities(collection, repo);
            InsertEntities(collection, repo);
        }

        // note: could probably make some of this functino an bit more generic but let's go with it for now.
        private void SetClientData()
        {
            var entities = Database.GetCollection<Client>($"firm[{_firmId}]::client");

            var queryable = entities.AsQueryable().Select(q => new Client()
            {
                AmountPaid = q.AmountPaid,
                ClientName = q.ClientName,
                Id = q.Id,
                Addresses = q.Addresses.Select(a => new Address()
                {
                    FirstLine = a.FirstLine,
                    SecondLine = a.SecondLine,
                    Postcode = a.Postcode,
                    ParentId = q.Id
                })
            });

            var list = queryable.ToList();

            var interceptor = new EntityInterceptor<Client>();
            var addressInterceptor = new ChildEntityInterceptor<Address>();

            var _entities = list.Select(e =>
            {
                e.PropertyChanged += Entity_PropertyChanged;

                var addressProxies = new List<Address>();
                if (e.Addresses != null)
                {
                    e.Addresses.ToList().ForEach(a =>
                    {
                        a.PropertyChanged += ChildEntity_PropertyChanged;
                        a.ParentId = e.Id;
                        addressProxies.Add(_generator.CreateClassProxyWithTarget(a, addressInterceptor));
                    });

                }
                e.Addresses = addressProxies;

                return _generator.CreateClassProxyWithTarget(e, interceptor);
            }).ToList();

            _clientRepo = new ClientRepository(_entities, _generator);
        }

        private void Entity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var changedId = ((Client)sender).Id;
            ((ClientRepository)_clientRepo).EntitiesWithStatus[changedId] = EntityStatus.Dirty;
        }

        private void ChildEntity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var changedId = ((Address)sender).ParentId;
            ((ClientRepository)_clientRepo).EntitiesWithStatus[changedId] = EntityStatus.Dirty;
        }
    }

}
