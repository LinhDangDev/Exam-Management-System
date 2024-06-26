using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class DotThiService
    {
        private readonly IDotThiRepository _dotThiRepository;
        public DotThiService(IDotThiRepository dotThiRepository)
        {
            _dotThiRepository = dotThiRepository;
        }
        public List<DotThi> GetAll()
        {
            List<DotThi> list = new List<DotThi>();
            using (IDataReader dataReader = _dotThiRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    DotThi dotThi = new DotThi();
                    dotThi.MaDotThi = dataReader.GetInt32(0);
                    dotThi.TenDotThi = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
                    dotThi.ThoiGianBatDau = dataReader.IsDBNull(2) ? null : dataReader.GetDateTime(2);
                    dotThi.ThoiGianKetThuc = dataReader.IsDBNull(3) ? null : dataReader.GetDateTime(3);
                    dotThi.NamHoc = dataReader.IsDBNull(4) ? null : dataReader.GetInt32(4);
                    list.Add(dotThi);
                }
                dataReader.Dispose();
            }
            return list;

        }
    }
}
