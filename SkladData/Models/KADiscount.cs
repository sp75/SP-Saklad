//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkladData.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KADiscount
    {
        public int KAId { get; set; }
        public int DiscForAll { get; set; }
        public decimal OnValue { get; set; }
        public int DiscCustom { get; set; }
    
        public virtual Kagent Kagent { get; set; }
    }
}
