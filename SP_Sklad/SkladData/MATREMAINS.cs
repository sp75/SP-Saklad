//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SP_Sklad.SkladData
{
    using System;
    using System.Collections.Generic;
    
    public partial class MatRemains
    {
        public int Id { get; set; }
        public int MatId { get; set; }
        public System.DateTime OnDate { get; set; }
        public decimal Remain { get; set; }
        public decimal Rsv { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public Nullable<decimal> MinPrice { get; set; }
        public Nullable<decimal> MaxPrice { get; set; }
        public Nullable<decimal> InWay { get; set; }
        public Nullable<decimal> Ordered { get; set; }
        public Nullable<decimal> ORsv { get; set; }
    
        public virtual Materials Materials { get; set; }
    }
}
