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
    
    public partial class v_PlannedCalculationDetDet
    {
        public System.Guid Id { get; set; }
        public string RecipeName { get; set; }
        public string MsrName { get; set; }
        public int RecId { get; set; }
        public Nullable<decimal> ProductionPlan { get; set; }
        public Nullable<decimal> PlannedProfitability { get; set; }
        public Nullable<decimal> RecipeOut { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.Guid PlannedCalculationId { get; set; }
        public Nullable<decimal> RecipeCount { get; set; }
        public Nullable<decimal> RecipePrice { get; set; }
        public Nullable<decimal> SalesPrice { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Profitability { get; set; }
        public Nullable<decimal> PlansPrice { get; set; }
        public int MatId { get; set; }
        public string MatGroupName { get; set; }
        public int GrpId { get; set; }
    }
}