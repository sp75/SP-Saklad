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
    
    public partial class WAREHOUSE
    {
        public WAREHOUSE()
        {
            this.POSREMAINS = new HashSet<POSREMAINS>();
            this.WAYBILLMOVE = new HashSet<WAYBILLMOVE>();
            this.WAYBILLMOVE1 = new HashSet<WAYBILLMOVE>();
            this.WAYBILLMAKE = new HashSet<WAYBILLMAKE>();
            this.USERACCESSWH = new HashSet<USERACCESSWH>();
            this.WaybillDet = new HashSet<WaybillDet>();
            this.WMatTurn = new HashSet<WMatTurn>();
            this.Materials = new HashSet<Materials>();
            this.Materials1 = new HashSet<Materials>();
            this.Materials2 = new HashSet<Materials>();
        }
    
        public int WID { get; set; }
        public string NAME { get; set; }
        public string ADDR { get; set; }
        public string NOTES { get; set; }
        public int DELETED { get; set; }
        public int DEF { get; set; }
    
        public virtual ICollection<POSREMAINS> POSREMAINS { get; set; }
        public virtual ICollection<WAYBILLMOVE> WAYBILLMOVE { get; set; }
        public virtual ICollection<WAYBILLMOVE> WAYBILLMOVE1 { get; set; }
        public virtual ICollection<WAYBILLMAKE> WAYBILLMAKE { get; set; }
        public virtual ICollection<USERACCESSWH> USERACCESSWH { get; set; }
        public virtual ICollection<WaybillDet> WaybillDet { get; set; }
        public virtual ICollection<WMatTurn> WMatTurn { get; set; }
        public virtual ICollection<Materials> Materials { get; set; }
        public virtual ICollection<Materials> Materials1 { get; set; }
        public virtual ICollection<Materials> Materials2 { get; set; }
    }
}
