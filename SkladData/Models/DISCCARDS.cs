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
    
    public partial class DISCCARDS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DISCCARDS()
        {
            this.WayBillDetAddProps = new HashSet<WayBillDetAddProps>();
        }
    
        public int CARDID { get; set; }
        public string NUM { get; set; }
        public int DISCTYPE { get; set; }
        public Nullable<System.DateTime> EXPIREDATE { get; set; }
        public int GRPID { get; set; }
        public int KAID { get; set; }
        public decimal ONVALUE { get; set; }
        public string NOTES { get; set; }
        public int DELETED { get; set; }
        public Nullable<decimal> STARTSALDO { get; set; }
    
        public virtual DISCCARDGRP DISCCARDGRP { get; set; }
        public virtual Kagent Kagent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillDetAddProps> WayBillDetAddProps { get; set; }
    }
}
