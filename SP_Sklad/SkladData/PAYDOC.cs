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
    
    public partial class PayDoc
    {
        public int PayDocId { get; set; }
        public int DocType { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<int> KaId { get; set; }
        public decimal Total { get; set; }
        public int PTypeId { get; set; }
        public int CurrId { get; set; }
        public int Deleted { get; set; }
        public string DocNum { get; set; }
        public int Checked { get; set; }
        public int WithNDS { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
        public Nullable<int> MPersonId { get; set; }
        public int CTypeId { get; set; }
        public Nullable<int> AccId { get; set; }
        public Nullable<int> CashId { get; set; }
        public Nullable<int> OperId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public string Schet { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public System.Guid Id { get; set; }
        public Nullable<int> ExDocType { get; set; }
        public Nullable<int> EntId { get; set; }
    
        public virtual CashDesks CashDesks { get; set; }
        public virtual ChargeType ChargeType { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual KAgentAccount KAgentAccount { get; set; }
        public virtual PayType PayType { get; set; }
        public virtual Kagent Kagent { get; set; }
        public virtual Kagent Kagent1 { get; set; }
    }
}
