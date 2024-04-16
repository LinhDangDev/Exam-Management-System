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

        public List<int> listDapAnKhoanh { get; set; }

        //public void TaoDSCauHoi()
        //{
        //    listSVKhoanh = new List<int>();
        //    for(int i = 0; i < 200; i++)
        //    {
        //        listSVKhoanh.Add(0);
        //    }
        //}
    }
}
