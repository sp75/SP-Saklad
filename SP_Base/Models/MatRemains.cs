namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MatRemains
    {
        public int Id { get; set; }

        public int MatId { get; set; }

        public DateTime OnDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Remain { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Rsv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AvgPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MinPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MaxPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? InWay { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Ordered { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ORsv { get; set; }

        public virtual Materials Materials { get; set; }
    }
}
