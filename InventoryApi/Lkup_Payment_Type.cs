//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lkup_Payment_Type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lkup_Payment_Type()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public decimal PAYMENT_TYPE_ID { get; set; }
        public string PAYMENT_TYPE_NAME { get; set; }
        public string NOTES { get; set; }
        public string ACTIVE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}