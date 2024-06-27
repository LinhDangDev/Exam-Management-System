using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class DeThiHoanViService
    {
        // có trường đặc biệt maDeThiNavigation - là đối tượng Đề Thi
        private readonly IDeThiHoanViRepository _deThiHoanViRepository;
        private readonly DeThiService _deThiService;
        public DeThiHoanViService(IDeThiHoanViRepository deThiHoanViRepository, DeThiService deThiService)
        {
            _deThiHoanViRepository = deThiHoanViRepository;
            _deThiService = deThiService;
        }
        private TblDeThiHoanVi getProperty(IDataReader dataReader)
        {
            TblDeThiHoanVi deThiHoanVi = new TblDeThiHoanVi();
            deThiHoanVi.MaDeHv = dataReader.GetInt64(0);
            deThiHoanVi.MaDeThi = dataReader.GetInt32(1);
            deThiHoanVi.KyHieuDe = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);
            deThiHoanVi.NgayTao = dataReader.GetDateTime(3);
            deThiHoanVi.Guid = dataReader.IsDBNull(4) ? null : dataReader.GetGuid(4);
            return deThiHoanVi;
        }
        public TblDeThiHoanVi SelectOne(long ma_de_hoan_vi)
        {
            TblDeThiHoanVi deThiHoanVi = new TblDeThiHoanVi();
            using(IDataReader dataReader = _deThiHoanViRepository.SelectOne(ma_de_hoan_vi))
            {
                if (dataReader.Read())
                {
                    deThiHoanVi = getProperty(dataReader);
                }
                TblDeThi deThi = _deThiService.SelectOne(deThiHoanVi.MaDeThi);
                // trường đặc biệt maDeThiNavigation - đối tượng là Đề thi
                deThiHoanVi.MaDeThiNavigation = deThi;
            }
            return deThiHoanVi;
        }
    }
}
