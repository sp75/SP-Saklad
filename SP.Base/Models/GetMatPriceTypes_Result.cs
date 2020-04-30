﻿namespace SP.Base.Models
{
    using System;

    public partial class GetMatPriceTypes_Result
    {
        public int PTypeId { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public Nullable<int> PPTypeId { get; set; }
        public Nullable<int> ExtraType { get; set; }
        public Nullable<int> WithNds { get; set; }
        public Nullable<int> Dis { get; set; }
        public Nullable<int> CurrId { get; set; }
        public Nullable<int> MatId { get; set; }
        public int IsIndividually { get; set; }
        public string PtName { get; set; }
        public string TypeName { get; set; }
    }
}
