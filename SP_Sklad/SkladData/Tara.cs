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
    
    public partial class Tara
    {
        public int TaraId { get; set; }
        public string Name { get; set; }
        public string Artikul { get; set; }
        public Nullable<int> Num { get; set; }
        public int Deleted { get; set; }
        public Nullable<int> GrpId { get; set; }
        public Nullable<int> WId { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string Notes { get; set; }
        public string InvNumber { get; set; }
    }
}
