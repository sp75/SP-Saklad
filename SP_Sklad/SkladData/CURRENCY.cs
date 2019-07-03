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
    
    public partial class Currency
    {
        public Currency()
        {
            this.CONTRACTS = new HashSet<CONTRACTS>();
            this.CONTRDET = new HashSet<CONTRDET>();
            this.WayBillSvc = new HashSet<WayBillSvc>();
            this.MoneySaldo = new HashSet<MoneySaldo>();
            this.MatGroupPrices = new HashSet<MatGroupPrices>();
            this.MatPrices = new HashSet<MatPrices>();
            this.Services = new HashSet<Services>();
            this.WaybillDet = new HashSet<WaybillDet>();
            this.WaybillList = new HashSet<WaybillList>();
            this.PriceList = new HashSet<PriceList>();
            this.PayDoc = new HashSet<PayDoc>();
            this.CurrencyRate = new HashSet<CurrencyRate>();
        }
    
        public int CurrId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Def { get; set; }
        public int Deleted { get; set; }
        public string RepShortName { get; set; }
        public string RepFracName { get; set; }
        public int CurType { get; set; }
    
        public virtual ICollection<CONTRACTS> CONTRACTS { get; set; }
        public virtual ICollection<CONTRDET> CONTRDET { get; set; }
        public virtual ICollection<WayBillSvc> WayBillSvc { get; set; }
        public virtual ICollection<MoneySaldo> MoneySaldo { get; set; }
        public virtual ICollection<MatGroupPrices> MatGroupPrices { get; set; }
        public virtual ICollection<MatPrices> MatPrices { get; set; }
        public virtual ICollection<Services> Services { get; set; }
        public virtual ICollection<WaybillDet> WaybillDet { get; set; }
        public virtual ICollection<WaybillList> WaybillList { get; set; }
        public virtual ICollection<PriceList> PriceList { get; set; }
        public virtual ICollection<PayDoc> PayDoc { get; set; }
        public virtual ICollection<CurrencyRate> CurrencyRate { get; set; }
    }
}
