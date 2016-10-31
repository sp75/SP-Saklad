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
    
    public partial class KAgentAccount
    {
        public KAgentAccount()
        {
            this.MoneySaldo = new HashSet<MoneySaldo>();
            this.PayDoc = new HashSet<PayDoc>();
        }
    
        public int AccId { get; set; }
        public int KAId { get; set; }
        public int TypeId { get; set; }
        public int BankId { get; set; }
        public string AccNum { get; set; }
        public Nullable<int> Def { get; set; }
    
        public virtual AccountType AccountType { get; set; }
        public virtual Banks Banks { get; set; }
        public virtual Kagent Kagent { get; set; }
        public virtual ICollection<MoneySaldo> MoneySaldo { get; set; }
        public virtual ICollection<PayDoc> PayDoc { get; set; }
    }
}
