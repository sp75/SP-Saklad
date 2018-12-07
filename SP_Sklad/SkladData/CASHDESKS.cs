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
    
    public partial class CashDesks
    {
        public CashDesks()
        {
            this.MoneySaldo = new HashSet<MoneySaldo>();
            this.UserAccessCashDesks = new HashSet<UserAccessCashDesks>();
            this.PayDoc = new HashSet<PayDoc>();
        }
    
        public int CashId { get; set; }
        public string Name { get; set; }
        public int Deleted { get; set; }
        public Nullable<int> Def { get; set; }
        public Nullable<int> EnterpriseId { get; set; }
    
        public virtual ICollection<MoneySaldo> MoneySaldo { get; set; }
        public virtual ICollection<UserAccessCashDesks> UserAccessCashDesks { get; set; }
        public virtual ICollection<PayDoc> PayDoc { get; set; }
    }
}
