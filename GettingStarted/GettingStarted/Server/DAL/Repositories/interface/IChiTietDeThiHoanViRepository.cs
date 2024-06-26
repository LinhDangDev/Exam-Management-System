using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IChiTietDeThiHoanViRepository
    {
        public IDataReader GetBy_MaDeHV(long maDeHV, int maNhom, int maChiTietCaThi);
        public IDataReader GetBy_MaDeHv(long maDeHV);
        public IDataReader SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom);
    }
}
