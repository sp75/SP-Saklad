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
    
    public partial class KAGENTDOC
    {
        public int KAID { get; set; }
        public string DOCNAME { get; set; }
        public string DOCNUM { get; set; }
        public string DOCSERIES { get; set; }
        public string DOCWHOPRODUCE { get; set; }
        public System.DateTime DOCDATE { get; set; }
    
        public virtual Kagent Kagent { get; set; }
    }
}
