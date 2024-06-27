using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IChiTietDeThiHoanViRepository
    {
        public IDataReader SelectBy_MaDeHV_MaNhom_MaChiTietCaThi_v3(long maDeHV, int maNhom, int maChiTietCaThi);
        public IDataReader SelectBy_MaDeHV(long maDeHV);
        public IDataReader SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom);
    }
}
