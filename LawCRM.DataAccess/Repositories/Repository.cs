using LawCRM.Domain.Base;
using LawCRM.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace LawCRM.DataAccess.Repositories
{
    internal abstract class Repository<T> : IRepository<T> where T : Entity
    {
        internal IDictionary<int, EntityStatus> EntitiesWithStatus;
        private ICollection<T> _entities;

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
        
        public Repository(ICollection<T> entities)
        {
            EntitiesWithStatus = new Dictionary<int, EntityStatus>();
            _entities = entities;
            foreach(var entity in _entities)
            {
                entity.PropertyChanged += Entity_PropertyChanged;
            }
        }

        private void Entity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var changedId = ((T)sender).Id;
            EntitiesWithStatus[changedId] = EntityStatus.Dirty;
        }

        public T this[int index]
        {
            get => Entities.FirstOrDefault(i => i.Id == index);
            set => this[index] = value;
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
    }
    
    internal enum EntityStatus
    {
        Scrubbed,
        Dirty,
        New
    }
}
