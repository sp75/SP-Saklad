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
    
    public partial class WAYBILLDETADDPROPS
    {
        public int POSID { get; set; }
        public string GTD { get; set; }
        public string CERTNUM { get; set; }
        public Nullable<System.DateTime> CERTDATE { get; set; }
        public string PRODUCER { get; set; }
        public Nullable<int> WARRANTYPERIOD { get; set; }
        public Nullable<int> WARRANTYPERIODTYPE { get; set; }
        public Nullable<int> CARDID { get; set; }
        public string NOTES { get; set; }
        public Nullable<int> WBMAKED { get; set; }
    
        public virtual DISCCARDS DISCCARDS { get; set; }
        public virtual WaybillList WaybillList { get; set; }
        public virtual WaybillDet WaybillDet { get; set; }
    }
}
