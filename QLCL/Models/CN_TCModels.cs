using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLCL.Models
{
    public class CN_TCModels
    {
        public string MaCongNhan { get; set; }
        public string MaTrieuChung { get; set; }

        public CongNhanModels CongNhan { get; set; }
        public TrieuChungModels TrieuChung { get; set; }
    }
}
