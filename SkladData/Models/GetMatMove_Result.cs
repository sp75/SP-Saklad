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
    
    public partial class GetMatMove_Result
    {
        public Nullable<int> WBillId { get; set; }
        public Nullable<int> WType { get; set; }
        public Nullable<System.DateTime> OnDate { get; set; }
        public string Num { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string CurrName { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public Nullable<int> PosId { get; set; }
        public string KAgentName { get; set; }
        public string FromWh { get; set; }
        public Nullable<int> FromWId { get; set; }
        public string ToWh { get; set; }
        public Nullable<int> ToWId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<decimal> Remain { get; set; }
        public string MsrName { get; set; }
        public Nullable<decimal> SumIn { get; set; }
        public Nullable<decimal> SumOut { get; set; }
        public Nullable<decimal> AmountIn { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public string TypeName { get; set; }
        public string WhName { get; set; }
    }
}
