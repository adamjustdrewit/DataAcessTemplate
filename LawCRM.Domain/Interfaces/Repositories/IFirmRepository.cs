using LawCRM.Domain.Entities;
using System.Collections.Generic;

namespace LawCRM.Domain.Interfaces.Repositories
{
    public interface IFirmRepository : IRepository<Firm>
    {
        IEnumerable<Firm> GetTopClients(int count);
    }
}
