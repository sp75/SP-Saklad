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
    
    public partial class TechProcDet
    {
        public int DetId { get; set; }
        public int WbillId { get; set; }
        public System.DateTime OnDate { get; set; }
        public decimal Out { get; set; }
        public int ProcId { get; set; }
        public string Notes { get; set; }
        public Nullable<int> PersonId { get; set; }
    
        public virtual TechProcess TechProcess { get; set; }
        public virtual WaybillList WaybillList { get; set; }
        public virtual Kagent Kagent { get; set; }
    }
}
