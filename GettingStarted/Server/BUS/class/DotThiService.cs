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
        private DotThi getProperty(IDataReader dataReader)
        {
            DotThi dotThi = new DotThi();
            dotThi.MaDotThi = dataReader.GetInt32(0);
            dotThi.TenDotThi = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
            dotThi.ThoiGianBatDau = dataReader.IsDBNull(2) ? null : dataReader.GetDateTime(2);
            dotThi.ThoiGianKetThuc = dataReader.IsDBNull(3) ? null : dataReader.GetDateTime(3);
            dotThi.NamHoc = dataReader.IsDBNull(4) ? null : dataReader.GetInt32(4);
            return dotThi;
        }
        public List<DotThi> GetAll()
        {
            List<DotThi> result = new List<DotThi>();
            using (IDataReader dataReader = _dotThiRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    DotThi dotThi = getProperty(dataReader);
                    result.Add(dotThi);
                }
            }
            return result;
        }
        public DotThi SelectOne(int ma_dot_thi)
        {
            DotThi dotThi = new DotThi();
            using(IDataReader dataReader = _dotThiRepository.SelectOne(ma_dot_thi))
            {
                if (dataReader.Read())
                {
                    dotThi = getProperty(dataReader);
                }
            }
            return dotThi;
        }
    }
}
