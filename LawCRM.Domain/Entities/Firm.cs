using Template.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel;

namespace Template.Domain.Entities
{
    public class Client : Entity
    {
        public virtual string ClientName { get; set; }
        public virtual decimal AmountPaid { get; set; }

        public virtual IEnumerable<Address> Addresses { get; set; }
    }

    public class Address : ChildEntity
    {
        public virtual string FirstLine { get; set; }
        public virtual string SecondLine { get; set; }
        public virtual string Postcode { get; set; }        
    }
}
