using LawCRM.Domain.Base;
using System.Collections.Generic;

namespace LawCRM.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);

        void Remove(T entity);
        
        T this[int index]
        {
            get;
            set;
        }

        ICollection<T> Entities { get; set; }
    }
}
