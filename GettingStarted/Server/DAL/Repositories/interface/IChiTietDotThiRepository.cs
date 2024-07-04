using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IChiTietDotThiResposity
    {
        public IDataReader SelectBy_MaDotThi(int ma_dot_thi);
        public IDataReader SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao);
        public IDataReader SelectOne(int ma_chi_tiet_dot_thi);
    }
}
