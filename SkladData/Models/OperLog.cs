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
    
    public partial class OperLog
    {
        public int OpId { get; set; }
        public string OpCode { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<int> TabId { get; set; }
        public Nullable<int> Id { get; set; }
        public string DataBefore { get; set; }
        public string DataAfter { get; set; }
        public Nullable<int> FunId { get; set; }
        public string DocNum { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual Tables Tables { get; set; }
        public virtual Users Users { get; set; }
    }
}
