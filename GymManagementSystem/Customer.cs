//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GymManagementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public Customer()
        {
            this.CustomerMemberships = new HashSet<CustomerMembership>();
        }
    
        public int C_ID { get; set; }
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
    
        public virtual ICollection<CustomerMembership> CustomerMemberships { get; set; }
    }
}