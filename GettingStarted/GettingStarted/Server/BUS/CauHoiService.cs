using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class CauHoiService
    {
        private readonly ICauHoiRepository _cauHoiRepository;
        public CauHoiService(ICauHoiRepository cauHoiRepository)
        {
            _cauHoiRepository = cauHoiRepository;
        }
        private TblCauHoi getProperty(IDataReader dataReader)
        {
            TblCauHoi cauHoi = new TblCauHoi();
            cauHoi.MaCauHoi = dataReader.GetInt32(0);
            cauHoi.TieuDe = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
            cauHoi.KieuNoiDung = dataReader.GetInt32(2);
            cauHoi.NoiDung = dataReader.IsDBNull(3) ? null : dataReader.GetString(3);
            cauHoi.GhiChu = dataReader.IsDBNull(4) ? null : dataReader.GetString(4);
            cauHoi.HoanVi = dataReader.IsDBNull(5) ? null : dataReader.GetBoolean(5);
            return cauHoi;
        }
        public TblCauHoi SelectOne(int ma_cau_hoi)
        {
            TblCauHoi cauHoi = new TblCauHoi();
            using(IDataReader dataReader = _cauHoiRepository.SelectOne(ma_cau_hoi))
            {
                if (dataReader.Read())
                {
                    cauHoi = getProperty(dataReader);
                }
                dataReader.Dispose();
            }
            return cauHoi;
        }
    }
}
