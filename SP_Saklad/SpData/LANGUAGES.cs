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
    
    public partial class LANGUAGES
    {
        public LANGUAGES()
        {
            this.VIEWLNG = new HashSet<VIEWLNG>();
            this.REPLNG = new HashSet<REPLNG>();
        }
    
        public int LANGID { get; set; }
        public string SHORTNAME { get; set; }
        public string NAME { get; set; }
        public Nullable<int> FLAGS { get; set; }
    
        public virtual ICollection<VIEWLNG> VIEWLNG { get; set; }
        public virtual ICollection<REPLNG> REPLNG { get; set; }
    }
}
