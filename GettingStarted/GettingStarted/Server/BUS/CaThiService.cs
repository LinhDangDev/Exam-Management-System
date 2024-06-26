using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class CaThiService
    {
        private readonly ICaThiRepository _caThiRepository;
        public CaThiService(ICaThiRepository caThiRepository)
        {
            _caThiRepository = caThiRepository;
        }
        private CaThi getProperty(IDataReader dataReader)
        {
            CaThi caThi = new CaThi();
            caThi.MaCaThi = dataReader.GetInt32(0);
            caThi.TenCaThi = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
            caThi.MaChiTietDotThi = dataReader.GetInt32(2);
            caThi.ThoiGianBatDau = dataReader.GetDateTime(3);
            caThi.MaDeThi = dataReader.GetInt32(4);
            caThi.IsActivated = dataReader.GetBoolean(5);
            caThi.ActivatedDate = dataReader.IsDBNull(6) ? null : dataReader.GetDateTime(6);
            caThi.ThoiGianThi = dataReader.GetInt32(7);
            caThi.KetThuc = dataReader.GetBoolean(8);
            caThi.ThoiDiemKetThuc = dataReader.IsDBNull(9) ? null : dataReader.GetDateTime(9);
            caThi.MatMa =  dataReader.IsDBNull(10) ? null : dataReader.GetString(10);
            caThi.Approved = dataReader.GetBoolean(11);
            caThi.ApprovedDate = dataReader.IsDBNull(12) ? null : dataReader.GetDateTime(12);
            caThi.ApprovedComments = dataReader.IsDBNull(13) ? null : dataReader.GetString(13);
            return caThi;
        }
        public List<CaThi> SelectBy_ma_chi_tiet_dot_thi(int ma_chi_tiet_dot_thi)
        {
            List<CaThi> list = new List<CaThi>();
            using(IDataReader dataReader = _caThiRepository.SelectBy_ma_chi_tiet_dot_thi(ma_chi_tiet_dot_thi))
            {
                while (dataReader.Read())
                {
                    CaThi caThi = getProperty(dataReader);
                    list.Add(caThi);
                }
                dataReader.Dispose();
            }
            return list;
        }
        public CaThi SelectOne(int ma_ca_thi)
        {
            CaThi caThi = new CaThi();
            using(IDataReader dataReader = _caThiRepository.SelectOne(ma_ca_thi))
            {
                if (dataReader.Read())
                {
                    caThi = getProperty(dataReader);
                }
                dataReader.Dispose();
            }
            return caThi;
        }
    }
}
