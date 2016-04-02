//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SP_Saklad.SpData
{
    using System;
    using System.Collections.Generic;
    
    public partial class MATERIALS
    {
        public MATERIALS()
        {
            this.CONTRDET = new HashSet<CONTRDET>();
            this.DEBONINGDET = new HashSet<DEBONINGDET>();
            this.KAMATDISCOUNT = new HashSet<KAMATDISCOUNT>();
            this.MATCHANGE = new HashSet<MATCHANGE>();
            this.MATCHANGE1 = new HashSet<MATCHANGE>();
            this.MATREMAINS = new HashSet<MATREMAINS>();
            this.POSREMAINS = new HashSet<POSREMAINS>();
            this.WMATTURN = new HashSet<WMATTURN>();
            this.MATRECIPE = new HashSet<MATRECIPE>();
            this.MATPRICES = new HashSet<MATPRICES>();
            this.TAXWBDET = new HashSet<TAXWBDET>();
            this.WaybillDet = new HashSet<WaybillDet>();
        }
    
        public int MATID { get; set; }
        public string NAME { get; set; }
        public int MID { get; set; }
        public Nullable<int> NUM { get; set; }
        public int DEF { get; set; }
        public int DELETED { get; set; }
        public Nullable<int> GRPID { get; set; }
        public Nullable<int> WID { get; set; }
        public Nullable<decimal> MINRESERV { get; set; }
        public Nullable<int> CID { get; set; }
        public Nullable<int> DEMANDCAT { get; set; }
        public string BARCODE { get; set; }
        public string PRODUCER { get; set; }
        public Nullable<decimal> WEIGHT { get; set; }
        public Nullable<decimal> MSIZE { get; set; }
        public Nullable<decimal> NDS { get; set; }
        public Nullable<int> SERIALS { get; set; }
        public Nullable<int> ARCHIVED { get; set; }
        public string ARTIKUL { get; set; }
        public string LABELDESCR { get; set; }
        public Nullable<int> TYPEID { get; set; }
        public Nullable<System.DateTime> DATEADDED { get; set; }
        public Nullable<System.DateTime> DATEMODIFIED { get; set; }
        public string NOTES { get; set; }
        public string CF1 { get; set; }
        public string CF2 { get; set; }
        public string CF3 { get; set; }
        public string CF4 { get; set; }
        public string CF5 { get; set; }
        public byte[] BMP { get; set; }
    
        public virtual ICollection<CONTRDET> CONTRDET { get; set; }
        public virtual COUNTRIES COUNTRIES { get; set; }
        public virtual ICollection<DEBONINGDET> DEBONINGDET { get; set; }
        public virtual ICollection<KAMATDISCOUNT> KAMATDISCOUNT { get; set; }
        public virtual ICollection<MATCHANGE> MATCHANGE { get; set; }
        public virtual ICollection<MATCHANGE> MATCHANGE1 { get; set; }
        public virtual MATGROUP MATGROUP { get; set; }
        public virtual MATGROUP MATGROUP1 { get; set; }
        public virtual MATGROUP MATGROUP2 { get; set; }
        public virtual WAREHOUSE WAREHOUSE { get; set; }
        public virtual WAREHOUSE WAREHOUSE1 { get; set; }
        public virtual WAREHOUSE WAREHOUSE2 { get; set; }
        public virtual ICollection<MATREMAINS> MATREMAINS { get; set; }
        public virtual ICollection<POSREMAINS> POSREMAINS { get; set; }
        public virtual ICollection<WMATTURN> WMATTURN { get; set; }
        public virtual MEASURES MEASURES { get; set; }
        public virtual ICollection<MATRECIPE> MATRECIPE { get; set; }
        public virtual ICollection<MATPRICES> MATPRICES { get; set; }
        public virtual ICollection<TAXWBDET> TAXWBDET { get; set; }
        public virtual ICollection<WaybillDet> WaybillDet { get; set; }
    }
}
