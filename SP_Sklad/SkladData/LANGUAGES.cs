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
    
    public partial class Languages
    {
        public Languages()
        {
            this.RepLng = new HashSet<RepLng>();
            this.ViewLng = new HashSet<ViewLng>();
        }
    
        public int LangId { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public Nullable<int> Flags { get; set; }
    
        public virtual ICollection<RepLng> RepLng { get; set; }
        public virtual ICollection<ViewLng> ViewLng { get; set; }
    }
}
