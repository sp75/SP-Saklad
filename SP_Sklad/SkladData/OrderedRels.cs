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
    
    public partial class OrderedRels
    {
        public int Id { get; set; }
        public int OrdPosId { get; set; }
        public int OutPosId { get; set; }
    
        public virtual WaybillDet WaybillDet { get; set; }
        public virtual WaybillDet WaybillDet1 { get; set; }
    }
}
