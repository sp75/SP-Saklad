//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkladData.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KAgentDoc
    {
        public int KAId { get; set; }
        public string DocName { get; set; }
        public string DocNum { get; set; }
        public string DocSeries { get; set; }
        public string DocWhoProduce { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
    
        public virtual Kagent Kagent { get; set; }
    }
}
