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
    
    public partial class Lkup_UOI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lkup_UOI()
        {
            this.IssueDetails = new HashSet<IssueDetail>();
            this.Items = new HashSet<Item>();
        }
    
        public decimal UOI_ID { get; set; }
        public string UOI_NAME_AR { get; set; }
        public string UOI_NAME_EN { get; set; }
        public string ACTIVE { get; set; }
        public string NOTES { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IssueDetail> IssueDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Items { get; set; }
    }
}
