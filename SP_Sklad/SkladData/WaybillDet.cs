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
    
    public partial class WaybillDet
    {
        public WaybillDet()
        {
            this.POSREMAINS = new HashSet<POSREMAINS>();
            this.RETURNREL = new HashSet<RETURNREL>();
            this.RETURNREL1 = new HashSet<RETURNREL>();
            this.RETURNREL2 = new HashSet<RETURNREL>();
            this.RETURNREL3 = new HashSet<RETURNREL>();
            this.SERIALS = new HashSet<SERIALS>();
            this.WAYBILLDETTAXES = new HashSet<WAYBILLDETTAXES>();
            this.WaybillDet1 = new HashSet<WaybillDet>();
            this.WaybillDet2 = new HashSet<WaybillDet>();
            this.WaybillDet11 = new HashSet<WaybillDet>();
            this.WaybillDet3 = new HashSet<WaybillDet>();
            this.WMatTurn = new HashSet<WMatTurn>();
            this.WMatTurn1 = new HashSet<WMatTurn>();
        }
    
        public int PosId { get; set; }
        public int WbillId { get; set; }
        public int MatId { get; set; }
        public Nullable<int> WId { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Nds { get; set; }
        public Nullable<int> CurrId { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public Nullable<int> PtypeId { get; set; }
        public int Num { get; set; }
        public Nullable<int> Checked { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> BasePrice { get; set; }
        public Nullable<System.DateTime> Expires { get; set; }
        public Nullable<int> PosKind { get; set; }
        public Nullable<int> PosParent { get; set; }
        public Nullable<int> MsrUnitId { get; set; }
        public Nullable<int> DiscountKind { get; set; }
        public Nullable<decimal> AVG_IN_PICE { get; set; }
    
        public virtual ICollection<POSREMAINS> POSREMAINS { get; set; }
        public virtual ICollection<RETURNREL> RETURNREL { get; set; }
        public virtual ICollection<RETURNREL> RETURNREL1 { get; set; }
        public virtual ICollection<RETURNREL> RETURNREL2 { get; set; }
        public virtual ICollection<RETURNREL> RETURNREL3 { get; set; }
        public virtual ICollection<SERIALS> SERIALS { get; set; }
        public virtual WAREHOUSE WAREHOUSE { get; set; }
        public virtual WaybillList WaybillList { get; set; }
        public virtual WAYBILLDETADDPROPS WAYBILLDETADDPROPS { get; set; }
        public virtual ICollection<WAYBILLDETTAXES> WAYBILLDETTAXES { get; set; }
        public virtual ICollection<WaybillDet> WaybillDet1 { get; set; }
        public virtual ICollection<WaybillDet> WaybillDet2 { get; set; }
        public virtual ICollection<WaybillDet> WaybillDet11 { get; set; }
        public virtual ICollection<WaybillDet> WaybillDet3 { get; set; }
        public virtual ICollection<WMatTurn> WMatTurn { get; set; }
        public virtual ICollection<WMatTurn> WMatTurn1 { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Materials Materials { get; set; }
        public virtual PriceTypes PriceTypes { get; set; }
    }
}
