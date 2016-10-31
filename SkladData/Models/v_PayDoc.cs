//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkladData.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class v_PayDoc
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
        public string CurrName { get; set; }
        public string KaName { get; set; }
        public string KaFullName { get; set; }
        public string KaPhone { get; set; }
        public string PersonName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string PostIndex { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string www { get; set; }
        public Nullable<int> KType { get; set; }
        public string ChargeName { get; set; }
        public string PayTypeName { get; set; }
        public string CashdName { get; set; }
        public string AccNum { get; set; }
        public string BankName { get; set; }
        public string KAU { get; set; }
    }
}
