//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkladData.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TAXWBDET
    {
        public int TWBDETID { get; set; }
        public int TWBID { get; set; }
        public int MATID { get; set; }
        public decimal AMOUNT { get; set; }
        public System.DateTime ONDATE { get; set; }
        public decimal PRICE { get; set; }
        public Nullable<decimal> NDS { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
    
        public virtual Materials Materials { get; set; }
        public virtual TAXWB TAXWB { get; set; }
    }
}
