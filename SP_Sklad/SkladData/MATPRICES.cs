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
    
    public partial class MATPRICES
    {
        public int PTYPEID { get; set; }
        public int MATID { get; set; }
        public decimal ONVALUE { get; set; }
        public Nullable<int> CURRID { get; set; }
        public int EXTRATYPE { get; set; }
        public int DIS { get; set; }
        public Nullable<int> WITHNDS { get; set; }
        public Nullable<int> PPTYPEID { get; set; }
    
        public virtual PRICETYPES PRICETYPES { get; set; }
        public virtual PRICETYPES PRICETYPES1 { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Currency Currency1 { get; set; }
        public virtual Materials Materials { get; set; }
    }
}
