using Template.DataAccess.Repositories;
using Template.Domain.Base;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Template.DataAccess.Storage
{
    internal abstract class MongoDataContext
    {
        protected MongoClientBase Client;
        protected IMongoDatabase Database;

        public MongoDataContext()
        {
            Client = new MongoClient();
            Database = Client.GetDatabase("somedatabasefromconnectionstrings");
        }

        protected void DeleteEntities<T>(IMongoCollection<T> collection, Repository<T> repo) where T : Entity
        {
            var deletes = repo.EntitiesWithStatus.Where(Functions.StatusIs(EntityStatus.Scrubbed)).Select(Functions.Key());
            collection.DeleteMany(d => deletes.Contains(d.Id));
        }

        protected void UpdateEntities<T>(IMongoCollection<T> collection, Repository<T> repo) where T : Entity
        {
            var updates = repo.EntitiesWithStatus.Where(Functions.StatusIs(EntityStatus.Dirty)).Select(Functions.Key());
            
            foreach (var updateId in updates)
            {
                var itemToReplace = repo[updateId];
                var replaceOneResult = collection.ReplaceOne(
                    doc => doc.Id == updateId,
                    itemToReplace
                );
            }
        }

        protected void InsertEntities<T>(IMongoCollection<T> collection, Repository<T> repo) where T : Entity
        {
            var adds = repo.EntitiesWithStatus.Where(Functions.StatusIs(EntityStatus.New)).Select(Functions.Key());
            List<T> addObjects = new List<T>();
            foreach (var addId in adds)
            {
                addObjects.Add(repo[addId]);
            }

            if (addObjects.Count > 0)
            {
                collection.InsertMany(addObjects);
            }
        }
    }
}
