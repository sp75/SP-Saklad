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
    
    public partial class Measures
    {
        public Measures()
        {
            this.Materials = new HashSet<Materials>();
            this.Services = new HashSet<Services>();
        }
    
        public int MId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Def { get; set; }
        public int Deleted { get; set; }
        public string Code { get; set; }
    
        public virtual ICollection<Materials> Materials { get; set; }
        public virtual ICollection<Services> Services { get; set; }
    }
}
