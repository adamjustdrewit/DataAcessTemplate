using Castle.DynamicProxy;
using Template.Domain.Base;
using Template.Domain.Interfaces.Data;
using System.Collections.Generic;
using System.Linq;

namespace Template.DataAccess.Repositories
{
    internal abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private ICollection<T> _entities;       
        public IDictionary<string, EntityStatus> EntitiesWithStatus { get; set; }

        public ICollection<T> Entities
        {
            get
            {
                var idsToIgnore = EntitiesWithStatus.Where(v => v.Value == EntityStatus.Scrubbed).Select(k => k.Key);
                return _entities.Where(e => !idsToIgnore.Contains(e.Id)).ToList();
            }
            set
            {
                _entities = value;
            }
        }
        
        public Repository(ICollection<T> entities, IProxyGenerator generator)
        {
            EntitiesWithStatus = new Dictionary<string, EntityStatus>();

            _entities = entities;
            
        }

        public T this[string id]
        {
            get => Entities.FirstOrDefault(i => i.Id == id);
            set => this[id] = value;
        }

        public void Add(T entity)
        {
            EntitiesWithStatus.Add(entity.Id, EntityStatus.New);
            _entities.Add(entity);
        }

        public void Remove(T entity)
        {
            var removedEntity = EntitiesWithStatus[entity.Id];
            removedEntity = EntityStatus.Scrubbed;
        }

        public ICollection<T> All()
        {
            return _entities;
        }
    }
    
    internal enum EntityStatus
    {
        Scrubbed,
        Dirty,
        New
    }
}
