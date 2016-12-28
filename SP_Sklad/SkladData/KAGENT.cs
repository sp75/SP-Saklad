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
    
    public partial class Kagent
    {
        public Kagent()
        {
            this.Commission = new HashSet<Commission>();
            this.Commission1 = new HashSet<Commission>();
            this.Commission2 = new HashSet<Commission>();
            this.Commission3 = new HashSet<Commission>();
            this.CONTRACTS = new HashSet<CONTRACTS>();
            this.CONTRACTS1 = new HashSet<CONTRACTS>();
            this.Customers = new HashSet<Customers>();
            this.DISCCARDS = new HashSet<DISCCARDS>();
            this.KaAddr = new HashSet<KaAddr>();
            this.KAMatDiscount = new HashSet<KAMatDiscount>();
            this.KAMatGroupDiscount = new HashSet<KAMatGroupDiscount>();
            this.PayDoc = new HashSet<PayDoc>();
            this.PayDoc1 = new HashSet<PayDoc>();
            this.TAXWB = new HashSet<TAXWB>();
            this.WaybillMove = new HashSet<WaybillMove>();
            this.KAgentAccount = new HashSet<KAgentAccount>();
            this.KAgentPersons = new HashSet<KAgentPersons>();
            this.Routes1 = new HashSet<Routes>();
            this.TAXWB1 = new HashSet<TAXWB>();
            this.TechProcDet = new HashSet<TechProcDet>();
            this.WayBillMake = new HashSet<WayBillMake>();
            this.WayBillSvc = new HashSet<WayBillSvc>();
            this.WaybillList = new HashSet<WaybillList>();
            this.WaybillList1 = new HashSet<WaybillList>();
            this.WaybillList2 = new HashSet<WaybillList>();
        }
    
        public int KaId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string INN { get; set; }
        public string OKPO { get; set; }
        public string CertNum { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string www { get; set; }
        public string Fax { get; set; }
        public int Deleted { get; set; }
        public string Notes { get; set; }
        public string KPP { get; set; }
        public int NdsPayer { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public int KType { get; set; }
        public int KaKind { get; set; }
        public string Job { get; set; }
        public Nullable<decimal> StartSaldo { get; set; }
        public Nullable<System.DateTime> StartSaldoDate { get; set; }
        public Nullable<int> PTypeId { get; set; }
        public Nullable<int> JobType { get; set; }
        public Nullable<int> Def { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> Archived { get; set; }
        public string KAU { get; set; }
        public string ContractType { get; set; }
        public Nullable<System.DateTime> ContractDate { get; set; }
        public string ContractNum { get; set; }
        public Nullable<int> RouteId { get; set; }
        public Nullable<System.Guid> GroupId { get; set; }
    
        public virtual ICollection<Commission> Commission { get; set; }
        public virtual ICollection<Commission> Commission1 { get; set; }
        public virtual ICollection<Commission> Commission2 { get; set; }
        public virtual ICollection<Commission> Commission3 { get; set; }
        public virtual ICollection<CONTRACTS> CONTRACTS { get; set; }
        public virtual ICollection<CONTRACTS> CONTRACTS1 { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<DISCCARDS> DISCCARDS { get; set; }
        public virtual ICollection<KaAddr> KaAddr { get; set; }
        public virtual KADiscount KADiscount { get; set; }
        public virtual ICollection<KAMatDiscount> KAMatDiscount { get; set; }
        public virtual ICollection<KAMatGroupDiscount> KAMatGroupDiscount { get; set; }
        public virtual ICollection<PayDoc> PayDoc { get; set; }
        public virtual ICollection<PayDoc> PayDoc1 { get; set; }
        public virtual ICollection<TAXWB> TAXWB { get; set; }
        public virtual ICollection<WaybillMove> WaybillMove { get; set; }
        public virtual ICollection<KAgentAccount> KAgentAccount { get; set; }
        public virtual KAgentDoc KAgentDoc { get; set; }
        public virtual ICollection<KAgentPersons> KAgentPersons { get; set; }
        public virtual KontragentGroup KontragentGroup { get; set; }
        public virtual Routes Routes { get; set; }
        public virtual ICollection<Routes> Routes1 { get; set; }
        public virtual ICollection<TAXWB> TAXWB1 { get; set; }
        public virtual ICollection<TechProcDet> TechProcDet { get; set; }
        public virtual ICollection<WayBillMake> WayBillMake { get; set; }
        public virtual ICollection<WayBillSvc> WayBillSvc { get; set; }
        public virtual ICollection<WaybillList> WaybillList { get; set; }
        public virtual ICollection<WaybillList> WaybillList1 { get; set; }
        public virtual ICollection<WaybillList> WaybillList2 { get; set; }
        public virtual Users Users { get; set; }
    }
}
