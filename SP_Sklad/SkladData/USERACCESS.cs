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
    
    public partial class UserAccess
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FunId { get; set; }
        public int CanView { get; set; }
        public int CanInsert { get; set; }
        public int CanModify { get; set; }
        public int CanDelete { get; set; }
        public int CanPost { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Functions Functions { get; set; }
    }
}
