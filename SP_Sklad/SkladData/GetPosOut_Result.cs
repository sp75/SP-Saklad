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
    
    public partial class GetPosOut_Result
    {
        public int PosId { get; set; }
        public int WbillId { get; set; }
        public int WType { get; set; }
        public string Num { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> KaId { get; set; }
        public string KaName { get; set; }
        public int WID { get; set; }
        public string WhName { get; set; }
        public int MatId { get; set; }
        public string MatName { get; set; }
        public string Artikul { get; set; }
        public decimal Amount { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public Nullable<int> CurrId { get; set; }
        public string CurrNmae { get; set; }
        public int Checked { get; set; }
        public string Measure { get; set; }
        public Nullable<decimal> Nds { get; set; }
        public string BarCode { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> BasePrice { get; set; }
        public Nullable<decimal> ReturnAmount { get; set; }
    }
}
