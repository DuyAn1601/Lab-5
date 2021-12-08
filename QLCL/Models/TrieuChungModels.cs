using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLCL.Models
{
    public class TrieuChungModels
    {
        public string MaTrieuChung;
        public string TenTrieuChung;

        public ICollection<CN_TCModels> CN_TCs { get; set; }
    }
}
