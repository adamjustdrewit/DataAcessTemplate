using Template.Domain.Base;
using System.Collections.Generic;

namespace Template.Domain.Interfaces.Data
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);

        void Remove(T entity);

        ICollection<T> All();
        
        T this[string id]
        {
            get;
            set;
        }
    }
}
