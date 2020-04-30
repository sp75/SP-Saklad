using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Base.Models
{
    using System;

    public partial class GetDirTree_Result
    {
        public Nullable<int> Id { get; set; }
        public Nullable<int> FunId { get; set; }
        public Nullable<int> PId { get; set; }
        public Nullable<decimal> Num { get; set; }
        public int GrpId { get; set; }
        public Nullable<int> ImageIndex { get; set; }
        public int IsGroup { get; set; }
        public Nullable<int> ShowInTree { get; set; }
        public Nullable<int> GType { get; set; }
        public Nullable<int> DisabledIndex { get; set; }
        public int ShowExpanded { get; set; }
        public string Name { get; set; }
        public Nullable<int> CanView { get; set; }
        public Nullable<int> CanInsert { get; set; }
        public Nullable<int> CanModify { get; set; }
        public Nullable<int> CanDelete { get; set; }
        public Nullable<int> CanPost { get; set; }
    }
}
