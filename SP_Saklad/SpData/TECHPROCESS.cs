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
    
    public partial class TECHPROCESS
    {
        public TECHPROCESS()
        {
            this.TECHPROCDET = new HashSet<TECHPROCDET>();
        }
    
        public int PROCID { get; set; }
        public string NAME { get; set; }
    
        public virtual ICollection<TECHPROCDET> TECHPROCDET { get; set; }
    }
}
