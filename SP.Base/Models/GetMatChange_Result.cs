
namespace SP.Base.Models
{
    using System;

    public partial class GetMatChange_Result
    {
        public int MatId { get; set; }
        public int ChangeId { get; set; }
        public string MatChangeName { get; set; }
        public string Artikul { get; set; }
        public string Notes { get; set; }
        public string GrpName { get; set; }
        public string MsrName { get; set; }
    }
}
