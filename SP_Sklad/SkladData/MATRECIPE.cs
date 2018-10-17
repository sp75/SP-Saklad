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
    
    public partial class MatRecipe
    {
        public MatRecipe()
        {
            this.WayBillMake = new HashSet<WayBillMake>();
            this.MatRecDet = new HashSet<MatRecDet>();
            this.MatRecipeTechProcDet = new HashSet<MatRecipeTechProcDet>();
            this.ProductionPlanDet = new HashSet<ProductionPlanDet>();
            this.SchedulingOrders = new HashSet<SchedulingOrders>();
            this.PlannedCalculationDet = new HashSet<PlannedCalculationDet>();
        }
    
        public int RecId { get; set; }
        public string Num { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public int MatId { get; set; }
        public Nullable<int> RType { get; set; }
        public decimal Out { get; set; }
    
        public virtual ICollection<WayBillMake> WayBillMake { get; set; }
        public virtual Materials Materials { get; set; }
        public virtual ICollection<MatRecDet> MatRecDet { get; set; }
        public virtual ICollection<MatRecipeTechProcDet> MatRecipeTechProcDet { get; set; }
        public virtual ICollection<ProductionPlanDet> ProductionPlanDet { get; set; }
        public virtual ICollection<SchedulingOrders> SchedulingOrders { get; set; }
        public virtual ICollection<PlannedCalculationDet> PlannedCalculationDet { get; set; }
    }
}
