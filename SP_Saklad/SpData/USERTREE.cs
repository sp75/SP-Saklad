//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SP_Saklad.SpData
{
    using System;
    using System.Collections.Generic;
    
    public partial class USERTREE
    {
        public USERTREE()
        {
            this.USERTREEVIEW = new HashSet<USERTREEVIEW>();
        }
    
        public int TREEID { get; set; }
        public string NOTES { get; set; }
    
        public virtual ICollection<USERTREEVIEW> USERTREEVIEW { get; set; }
    }
}
