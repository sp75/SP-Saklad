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
    
    public partial class MatRecDet
    {
        public int DetId { get; set; }
        public int RecId { get; set; }
        public int MatId { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> Coefficient { get; set; }
    
        public virtual Materials Materials { get; set; }
        public virtual MatRecipe MatRecipe { get; set; }
    }
}
