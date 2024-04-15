using System.Numerics;

namespace GettingStarted.Client.DAL
{
    public class ApplicationDataService
    {
        //ma_sinh_vien có kiểu dữ liệu là BigInt, identity(1,1)
        public long? ma_sinh_vien { get; set; }
        //nó là MSSV
        public string? ma_so_sinh_vien { get; set; }
        //sv thi ca nào
        public int? ma_ca_thi{ get; set; }
        public long? ma_de_thi_hoan_vi { get; set; }
        public string? ten_mon_hoc { get; set; }

        public List<int> listSVKhoanh { get; set; }

        public void GetDSKhoanh(List<int> danhsach)
        {
            listSVKhoanh = new List<int>();
            listSVKhoanh.AddRange(danhsach);
        }
    }
}
