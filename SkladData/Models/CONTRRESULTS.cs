//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkladData.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CONTRRESULTS
    {
        public Nullable<decimal> SHIPPEDSUMM { get; set; }
        public Nullable<decimal> SHIPPEDAMOUNT { get; set; }
        public Nullable<decimal> PAIDSUMM { get; set; }
        public int CONTRID { get; set; }
    
        public virtual CONTRACTS CONTRACTS { get; set; }
    }
}
