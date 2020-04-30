
namespace SP.Base.Models
{
    using System;
    public partial class GetLastPrice_Result
    {
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> CurrId { get; set; }
        public Nullable<decimal> OnValue { get; set; }
    }
}
