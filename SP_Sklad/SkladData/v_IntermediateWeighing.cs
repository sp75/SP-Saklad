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
    
    public partial class v_IntermediateWeighing
    {
        public System.Guid Id { get; set; }
        public string Num { get; set; }
        public System.DateTime OnDate { get; set; }
        public int Checked { get; set; }
        public string Notes { get; set; }
        public int PersonId { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.Guid> SessionId { get; set; }
        public int WbillId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string RecipeName { get; set; }
        public string WbNum { get; set; }
        public string PersonName { get; set; }
        public int WbChecked { get; set; }
    }
}
