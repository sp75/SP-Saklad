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
    
    public partial class Customers
    {
        public System.Guid Id { get; set; }
        public int KaId { get; set; }
        public string Ligin { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<bool> IsOnline { get; set; }
    
        public virtual Kagent Kagent { get; set; }
    }
}
