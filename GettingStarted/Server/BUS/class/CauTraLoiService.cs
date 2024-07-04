using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class CauTraLoiService
    {
        private readonly ICauTraLoiRepository _cauTraLoiRepository;
        public CauTraLoiService(ICauTraLoiRepository cauTraLoiRepository)
        {
            _cauTraLoiRepository = cauTraLoiRepository;
        }
        private TblCauTraLoi getProperty(IDataReader dataReader)
        {
            TblCauTraLoi cauTraLoi = new TblCauTraLoi();
            cauTraLoi.MaCauTraLoi = dataReader.GetInt32(0);
            cauTraLoi.MaCauHoi = dataReader.GetInt32(1);
            cauTraLoi.ThuTu = dataReader.GetInt32(2);
            cauTraLoi.NoiDung = dataReader.IsDBNull(3) ? null : dataReader.GetString(3);
            cauTraLoi.LaDapAn = dataReader.GetBoolean(4);
            cauTraLoi.HoanVi = dataReader.GetBoolean(5);
            return cauTraLoi;
        }
        public List<TblCauTraLoi> SelectBy_MaCauHoi(int ma_cau_hoi)
        {
            List<TblCauTraLoi> list = new List<TblCauTraLoi>();
            using (IDataReader dataReader = _cauTraLoiRepository.SelectBy_MaCauHoi(ma_cau_hoi))
            {
                while (dataReader.Read())
                {
                    TblCauTraLoi cauTraLoi = getProperty(dataReader);
                    list.Add(cauTraLoi);
                }
            }
            return list;
        }

    }
}
