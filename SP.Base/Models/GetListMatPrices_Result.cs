namespace SP.Base.Models
{
    using System;

    public partial class GetListMatPrices_Result
    {
        public Nullable<int> PType { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> CurrId { get; set; }
        public string CurrName { get; set; }
        public Nullable<double> Dis { get; set; }
    }
}
