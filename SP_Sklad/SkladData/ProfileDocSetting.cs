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
    
    public partial class ProfileDocSetting
    {
        public int ProfileId { get; set; }
        public int DocType { get; set; }
        public string Name { get; set; }
        public Nullable<int> AllowZero { get; set; }
        public Nullable<decimal> DefAmount { get; set; }
        public Nullable<int> AutoNum { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public Nullable<int> Checked { get; set; }
        public int Num { get; set; }
    }
}
