//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkladData.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WayBillDetAddProps
    {
        public int PosId { get; set; }
        public string GTD { get; set; }
        public string CertNum { get; set; }
        public Nullable<System.DateTime> CertDate { get; set; }
        public string Producer { get; set; }
        public Nullable<int> WarrantyPeriod { get; set; }
        public Nullable<int> WarrantyPeriodType { get; set; }
        public Nullable<int> CardId { get; set; }
        public string Notes { get; set; }
        public Nullable<int> WbMaked { get; set; }
    
        public virtual DISCCARDS DISCCARDS { get; set; }
        public virtual WaybillDet WaybillDet { get; set; }
        public virtual WaybillList WaybillList { get; set; }
    }
}
