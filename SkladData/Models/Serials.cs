//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkladData.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Serials
    {
        public int SId { get; set; }
        public int PosId { get; set; }
        public string SerialNo { get; set; }
        public string InvNumb { get; set; }
    
        public virtual WaybillDet WaybillDet { get; set; }
    }
}
