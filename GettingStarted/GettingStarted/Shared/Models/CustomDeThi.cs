using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingStarted.Shared.Models
{
    public class CustomDeThi
    {
        public int MaNhom { get; set; }
        public int MaCauHoi { get; set; }
        public string? NoiDungCauHoiNhomCha { get; set; }
        public string? NoiDungCauHoiNhom { get; set; }
        public string? NoiDungCauHoi { get; set; }
        public int KieuNoiDungCauHoi { get; set; }
        public Dictionary<int, string?>? CauTraLois { get; set; }

    }
}
