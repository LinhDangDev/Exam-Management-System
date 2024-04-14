using GettingStarted.Server.DAL.DataReader;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class DotThiRepository : IDotThiRepository
    {
        public IDataReader GetAll()
        {
            DatabaseReader sql = new DatabaseReader("dot_thi_GetAll");
            return sql.ExcuteReader();
        }
    }
}
