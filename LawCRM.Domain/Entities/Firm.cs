using LawCRM.Domain.Base;

namespace LawCRM.Domain.Entities
{
    public class Firm : Entity
    {
        public string FirmName { get; set; }
        public decimal Revenue { get; set; }
    }
}
