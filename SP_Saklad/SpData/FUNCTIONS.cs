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
    
    public partial class FUNCTIONS
    {
        public FUNCTIONS()
        {
            this.USERTREEVIEW = new HashSet<USERTREEVIEW>();
            this.USERACCESS = new HashSet<USERACCESS>();
        }
    
        public int FUNID { get; set; }
        public string CLASSNAME { get; set; }
        public Nullable<int> TABID { get; set; }
        public int LOGGING { get; set; }
        public int FLAGS { get; set; }
    
        public virtual TABLES TABLES { get; set; }
        public virtual ICollection<USERTREEVIEW> USERTREEVIEW { get; set; }
        public virtual ICollection<USERACCESS> USERACCESS { get; set; }
    }
}
