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
    
    public partial class TAXWB
    {
        public TAXWB()
        {
            this.TAXWBDET = new HashSet<TAXWBDET>();
        }
    
        public int TWBID { get; set; }
        public int KAID { get; set; }
        public System.DateTime ONDATE { get; set; }
        public Nullable<decimal> ADDCHARGES { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public int DEFNUM { get; set; }
        public int DELETED { get; set; }
        public int CHECKED { get; set; }
        public Nullable<decimal> SUMMALL { get; set; }
        public string CONDITION { get; set; }
        public string FORM { get; set; }
        public Nullable<int> PERSONID { get; set; }
        public Nullable<decimal> NDS { get; set; }
        public string NUM { get; set; }
        public Nullable<int> DOCID { get; set; }
        public Nullable<int> ENTID { get; set; }
        public string NUM1 { get; set; }
        public string NUM2 { get; set; }
        public string CONTRACT_TYPE { get; set; }
        public Nullable<System.DateTime> CONTRACT_DATE { get; set; }
        public string CONTRACT_NUM { get; set; }
        public Nullable<int> TAXREESTRTYPE { get; set; }
    
        public virtual Kagent Kagent { get; set; }
        public virtual Kagent Kagent1 { get; set; }
        public virtual Docs Docs { get; set; }
        public virtual ICollection<TAXWBDET> TAXWBDET { get; set; }
    }
}
