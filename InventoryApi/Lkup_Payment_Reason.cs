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
    
    public partial class Lkup_Payment_Reason
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lkup_Payment_Reason()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public decimal REASON_ID { get; set; }
        public string REASON_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string ACTIVE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
