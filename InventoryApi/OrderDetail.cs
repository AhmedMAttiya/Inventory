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
    
    public partial class OrderDetail
    {
        public decimal ORDERDETAIL_ID { get; set; }
        public decimal ORDER_ID { get; set; }
        public decimal ITEM { get; set; }
        public decimal QUANTITY { get; set; }
        public decimal UNIT_PRICE { get; set; }
        public decimal TOTAL_PRICE { get; set; }
        public string NOTES { get; set; }
    
        public virtual Item Item1 { get; set; }
        public virtual Order Order { get; set; }
    }
}
