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
    
    public partial class TECHPROCDET
    {
        public int DETID { get; set; }
        public int WBILLID { get; set; }
        public System.DateTime ONDATE { get; set; }
        public decimal OUT { get; set; }
        public int PROCID { get; set; }
        public string NOTES { get; set; }
        public Nullable<int> PERSONID { get; set; }
    
        public virtual Kagent Kagent { get; set; }
        public virtual TECHPROCESS TECHPROCESS { get; set; }
        public virtual WaybillList WaybillList { get; set; }
    }
}
