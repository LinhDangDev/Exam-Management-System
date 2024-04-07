using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface IChiTietDeThiHoanViRepository
    {
        public IDataReader GetBy_MaDeHV(long maDeHV, int maNhom, int maChiTietCaThi);
        public IDataReader GetBy_MaDeHv(long maDeHV);
    }
}
