using System.Collections.Generic;
using Template.Domain.Entities;
using System.Linq;
using Template.Domain.Data;
using Castle.DynamicProxy;

namespace Template.DataAccess.Repositories
{
    class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ICollection<Client> entities, IProxyGenerator generator) : base(entities, generator)
        {
        }

        public IEnumerable<Client> GetTopClients(int count)
        {
            return Entities.OrderByDescending(c => c.AmountPaid).Take(count);
        }
    }
}
