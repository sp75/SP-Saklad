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
    
    public partial class ProductionPlanDet
    {
        public System.Guid Id { get; set; }
        public Nullable<int> MatId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.Guid> ProductionPlanId { get; set; }
        public Nullable<decimal> Remain { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> WhId { get; set; }
    
        public virtual Materials Materials { get; set; }
        public virtual ProductionPlans ProductionPlans { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
