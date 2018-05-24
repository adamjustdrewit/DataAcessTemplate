using System.Collections.Generic;
using LawCRM.Domain.Entities;
using LawCRM.Domain.Interfaces.Repositories;
using System.Linq;

namespace LawCRM.DataAccess.Repositories
{
    class FirmRepository : Repository<Firm>, IFirmRepository
    {
        public FirmRepository(ICollection<Firm> entities) : base(entities)
        {
        }

        public IEnumerable<Firm> GetTopClients(int count)
        {
            return Entities.OrderByDescending(c => c.Revenue).Take(count);
        }
    }
}
