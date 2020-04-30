﻿namespace SP.Base.Models
{
    using System;

    public partial class GetMatList_Result
    {
        public int MatId { get; set; }
        public string Name { get; set; }
        public string Artikul { get; set; }
        public int MId { get; set; }
        public string Notes { get; set; }
        public Nullable<int> GrpId { get; set; }
        public Nullable<int> WId { get; set; }
        public Nullable<int> Num { get; set; }
        public Nullable<decimal> MinReserv { get; set; }
        public Nullable<int> CId { get; set; }
        public Nullable<int> DemandCat { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> MSize { get; set; }
        public string Producer { get; set; }
        public string BarCode { get; set; }
        public int MType { get; set; }
        public string ShortName { get; set; }
        public string CName { get; set; }
        public Nullable<int> Archived { get; set; }
        public string GrpName { get; set; }
        public string WhName { get; set; }
        public string DemandGroupName { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    }
}
