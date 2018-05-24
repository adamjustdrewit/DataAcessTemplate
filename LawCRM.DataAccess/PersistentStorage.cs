using LawCRM.DataAccess.Repositories;
using LawCRM.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LawCRM.DataAccess
{
    public class PersistentStorage
    {
        private FirmRepository firmRepo;
        public IFirmRepository FirmRepo
        {
            get { return firmRepo; }
            set { firmRepo = (FirmRepository)value; }
        }
        
        public void Submit()
        {
            var deletes = firmRepo.EntitiesWithStatus.Where(StatusIs(EntityStatus.Scrubbed));
            var adds = firmRepo.EntitiesWithStatus.Where(StatusIs(EntityStatus.New));
            var updates = firmRepo.EntitiesWithStatus.Where(StatusIs(EntityStatus.Dirty));
        }

        private Func<KeyValuePair<int, EntityStatus>, bool> StatusIs(EntityStatus status)
        {
            return s => s.Value == status;
        }
    }    
}
