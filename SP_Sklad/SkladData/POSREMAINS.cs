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
    
    public partial class PosRemains
    {
        public int Id { get; set; }
        public int PosId { get; set; }
        public int WId { get; set; }
        public int MatId { get; set; }
        public System.DateTime OnDate { get; set; }
        public decimal Remain { get; set; }
        public decimal Rsv { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public Nullable<decimal> InWay { get; set; }
        public Nullable<decimal> Ordered { get; set; }
    
        public virtual Materials Materials { get; set; }
        public virtual WaybillDet WaybillDet { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
