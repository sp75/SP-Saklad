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
    
    public partial class DISCCARDS
    {
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
        public virtual ICollection<WayBillDetAddProps> WayBillDetAddProps { get; set; }
    }
}
