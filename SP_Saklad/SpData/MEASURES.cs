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
    
    public partial class MEASURES
    {
        public MEASURES()
        {
            this.MATERIALS = new HashSet<MATERIALS>();
            this.SERVICES = new HashSet<SERVICES>();
        }
    
        public int MID { get; set; }
        public string NAME { get; set; }
        public string SHORTNAME { get; set; }
        public int DEF { get; set; }
        public int DELETED { get; set; }
        public string CODE { get; set; }
    
        public virtual ICollection<MATERIALS> MATERIALS { get; set; }
        public virtual ICollection<SERVICES> SERVICES { get; set; }
    }
}
