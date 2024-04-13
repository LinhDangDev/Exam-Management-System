using System.Data;

namespace GettingStarted.Server.DAL.Repositories 
{

    public interface ILopAoRepository
    {
        public IDataReader SelectOne(int ma_lop_ao);
        public IDataReader SelectBy_ma_mon_hoc(int ma_mon_hoc);
    }
}
