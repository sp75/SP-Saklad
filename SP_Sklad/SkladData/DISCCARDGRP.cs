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
    
    public partial class DiscCardGrp
    {
        public DiscCardGrp()
        {
            this.DiscCards = new HashSet<DiscCards>();
        }
    
        public int GrpId { get; set; }
        public string Name { get; set; }
        public int PId { get; set; }
        public Nullable<int> DiscType { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public Nullable<int> AutoNum { get; set; }
        public string Prefix { get; set; }
        public Nullable<int> CurrNum { get; set; }
        public string Suffix { get; set; }
        public string Notes { get; set; }
    
        public virtual ICollection<DiscCards> DiscCards { get; set; }
    }
}
