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
        public int Num { get; set; }
        public System.DateTime OnDate { get; set; }
        public decimal Out { get; set; }
        public int ProcId { get; set; }
        public string Notes { get; set; }
        public Nullable<int> PersonId { get; set; }
        public int WbillId { get; set; }
        public Nullable<int> MatId { get; set; }
        public Nullable<int> ExtMatAmount { get; set; }
        public Nullable<int> ExtMatId { get; set; }
        public Nullable<int> ExtMat2Id { get; set; }
        public Nullable<int> ExtMat2Amount { get; set; }
    
        public virtual Kagent Kagent { get; set; }
        public virtual WaybillList WaybillList { get; set; }
        public virtual TechProcess TechProcess { get; set; }
    }
}
