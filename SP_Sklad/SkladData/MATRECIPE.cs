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
            this.MatRecDet = new HashSet<MatRecDet>();
            this.WayBillMake = new HashSet<WayBillMake>();
        }
    
        public int RecId { get; set; }
        public string Num { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public int MatId { get; set; }
        public Nullable<int> RType { get; set; }
    
        public virtual Materials Materials { get; set; }
        public virtual ICollection<MatRecDet> MatRecDet { get; set; }
        public virtual ICollection<WayBillMake> WayBillMake { get; set; }
    }
}
