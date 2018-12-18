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
    
    public partial class Materials
    {
        public Materials()
        {
            this.CONTRDET = new HashSet<CONTRDET>();
            this.DeboningDet = new HashSet<DeboningDet>();
            this.MatRemains = new HashSet<MatRemains>();
            this.WMatTurn = new HashSet<WMatTurn>();
            this.MatRecipe = new HashSet<MatRecipe>();
            this.TAXWBDET = new HashSet<TAXWBDET>();
            this.KAMatDiscount = new HashSet<KAMatDiscount>();
            this.MatRecDet = new HashSet<MatRecDet>();
            this.MatPrices = new HashSet<MatPrices>();
            this.MatChange = new HashSet<MatChange>();
            this.MatChange1 = new HashSet<MatChange>();
            this.WaybillDet = new HashSet<WaybillDet>();
            this.WayBillMakeProps = new HashSet<WayBillMakeProps>();
            this.WayBillTmc = new HashSet<WayBillTmc>();
        }
    
        public int MatId { get; set; }
        public string Name { get; set; }
        public int MId { get; set; }
        public Nullable<int> Num { get; set; }
        public int Def { get; set; }
        public int Deleted { get; set; }
        public Nullable<int> GrpId { get; set; }
        public Nullable<int> WId { get; set; }
        public Nullable<decimal> MinReserv { get; set; }
        public Nullable<int> CId { get; set; }
        public Nullable<int> DemandCat { get; set; }
        public string BarCode { get; set; }
        public string Producer { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> MSize { get; set; }
        public Nullable<decimal> NDS { get; set; }
        public Nullable<int> Serials { get; set; }
        public Nullable<int> Archived { get; set; }
        public string Artikul { get; set; }
        public string LabelDescr { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string Notes { get; set; }
        public string CF1 { get; set; }
        public string CF2 { get; set; }
        public string CF3 { get; set; }
        public string CF4 { get; set; }
        public string CF5 { get; set; }
        public byte[] BMP { get; set; }
        public int DecPlaces { get; set; }
        public string InvNumber { get; set; }
        public string SerialNumber { get; set; }
    
        public virtual ICollection<CONTRDET> CONTRDET { get; set; }
        public virtual Countries Countries { get; set; }
        public virtual ICollection<DeboningDet> DeboningDet { get; set; }
        public virtual MatGroup MatGroup { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Warehouse Warehouse1 { get; set; }
        public virtual Warehouse Warehouse2 { get; set; }
        public virtual ICollection<MatRemains> MatRemains { get; set; }
        public virtual ICollection<WMatTurn> WMatTurn { get; set; }
        public virtual Measures Measures { get; set; }
        public virtual ICollection<MatRecipe> MatRecipe { get; set; }
        public virtual ICollection<TAXWBDET> TAXWBDET { get; set; }
        public virtual ICollection<KAMatDiscount> KAMatDiscount { get; set; }
        public virtual ICollection<MatRecDet> MatRecDet { get; set; }
        public virtual ICollection<MatPrices> MatPrices { get; set; }
        public virtual ICollection<MatChange> MatChange { get; set; }
        public virtual ICollection<MatChange> MatChange1 { get; set; }
        public virtual ICollection<WaybillDet> WaybillDet { get; set; }
        public virtual ICollection<WayBillMakeProps> WayBillMakeProps { get; set; }
        public virtual ICollection<WayBillTmc> WayBillTmc { get; set; }
    }
}
