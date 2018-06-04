using Template.Domain.Entities;
using Template.Domain.Interfaces.Data;
using System.Collections.Generic;

namespace Template.Domain.Data
{
    public interface IClientRepository : IRepository<Client>
    {
        IEnumerable<Client> GetTopClients(int count);
    }
}
