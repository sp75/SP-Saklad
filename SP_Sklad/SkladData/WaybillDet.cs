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
            this.ExtRel = new HashSet<ExtRel>();
            this.ExtRel1 = new HashSet<ExtRel>();
            this.PosRemains = new HashSet<PosRemains>();
            this.ReturnRel = new HashSet<ReturnRel>();
            this.ReturnRel1 = new HashSet<ReturnRel>();
            this.ReturnRel2 = new HashSet<ReturnRel>();
            this.ReturnRel3 = new HashSet<ReturnRel>();
            this.SERIALS = new HashSet<SERIALS>();
            this.WayBillDetTaxes = new HashSet<WayBillDetTaxes>();
            this.WMatTurn = new HashSet<WMatTurn>();
            this.WMatTurn1 = new HashSet<WMatTurn>();
            this.WaybillDet1 = new HashSet<WaybillDet>();
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
        public Nullable<decimal> AvgInPrice { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual ICollection<ExtRel> ExtRel { get; set; }
        public virtual ICollection<ExtRel> ExtRel1 { get; set; }
        public virtual Materials Materials { get; set; }
        public virtual ICollection<PosRemains> PosRemains { get; set; }
        public virtual PriceTypes PriceTypes { get; set; }
        public virtual ICollection<ReturnRel> ReturnRel { get; set; }
        public virtual ICollection<ReturnRel> ReturnRel1 { get; set; }
        public virtual ICollection<ReturnRel> ReturnRel2 { get; set; }
        public virtual ICollection<ReturnRel> ReturnRel3 { get; set; }
        public virtual ICollection<SERIALS> SERIALS { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual WayBillDetAddProps WayBillDetAddProps { get; set; }
        public virtual ICollection<WayBillDetTaxes> WayBillDetTaxes { get; set; }
        public virtual ICollection<WMatTurn> WMatTurn { get; set; }
        public virtual ICollection<WMatTurn> WMatTurn1 { get; set; }
        public virtual ICollection<WaybillDet> WaybillDet1 { get; set; }
        public virtual WaybillDet WaybillDet2 { get; set; }
        public virtual WaybillList WaybillList { get; set; }
    }
}
