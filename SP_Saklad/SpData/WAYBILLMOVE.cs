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
    
    public partial class WAYBILLMOVE
    {
        public int WBILLID { get; set; }
        public int SOURCEWID { get; set; }
        public Nullable<int> DESTWID { get; set; }
        public Nullable<int> PERSONID { get; set; }
    
        public virtual KAGENT KAGENT { get; set; }
        public virtual WAREHOUSE WAREHOUSE { get; set; }
        public virtual WAREHOUSE WAREHOUSE1 { get; set; }
        public virtual WaybillList WaybillList { get; set; }
    }
}
