using ASP_Examples.Lazy;
using System;

namespace ASP_Examples.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        // Virtual because a proxy can now override its behaviour
        public virtual string Name { get; set; }
        public virtual string ShippingAddress { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }

        public virtual byte[] ProfilePicture { get; set; }

        public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
    }
}
