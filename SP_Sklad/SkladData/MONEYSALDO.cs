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
    
    public partial class MONEYSALDO
    {
        public int SALDOTYPE { get; set; }
        public System.DateTime ONDATE { get; set; }
        public Nullable<int> ACCID { get; set; }
        public int CURRID { get; set; }
        public decimal SALDO { get; set; }
        public Nullable<int> CASHID { get; set; }
        public Nullable<decimal> SALDODEF { get; set; }
    
        public virtual KAGENTACCOUNT KAGENTACCOUNT { get; set; }
        public virtual CashDesks CashDesks { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
