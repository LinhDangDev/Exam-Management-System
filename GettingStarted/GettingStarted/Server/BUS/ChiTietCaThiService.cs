using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class ChiTietCaThiService
    {
        private readonly IChiTietCaThiRepository _chiTietCaThiRepository;
        public ChiTietCaThiService(IChiTietCaThiRepository chiTietCaThiRepository)
        {
            _chiTietCaThiRepository = chiTietCaThiRepository;
        }
        private ChiTietCaThi getProperty(IDataReader dataReader)
        {
            ChiTietCaThi chiTietCaThi = new ChiTietCaThi();
            chiTietCaThi.MaChiTietCaThi = dataReader.GetInt32(0);
            chiTietCaThi.MaCaThi = dataReader.IsDBNull(1) ? null : dataReader.GetInt32(1);
            chiTietCaThi.MaSinhVien = dataReader.IsDBNull(2) ? null : dataReader.GetInt64(2);
            chiTietCaThi.MaDeThi = dataReader.IsDBNull(3) ? null : dataReader.GetInt64(3);
            chiTietCaThi.ThoiGianBatDau = dataReader.IsDBNull(4) ? null : dataReader.GetDateTime(4);
            chiTietCaThi.ThoiGianKetThuc = dataReader.IsDBNull(5) ? null : dataReader.GetDateTime(5);
            chiTietCaThi.DaThi = dataReader.GetBoolean(6);
            chiTietCaThi.DaHoanThanh = dataReader.GetBoolean(7);
            chiTietCaThi.Diem = dataReader.GetDouble(8);
            chiTietCaThi.TongSoCau = dataReader.IsDBNull(9) ? null : dataReader.GetInt32(9);
            chiTietCaThi.SoCauDung = dataReader.IsDBNull(10) ? null : dataReader.GetInt32(10);
            chiTietCaThi.GioCongThem = dataReader.GetInt32(11);
            chiTietCaThi.ThoiDiemCong = dataReader.IsDBNull(12) ? null : dataReader.GetDateTime(12);
            chiTietCaThi.LyDoCong = dataReader.IsDBNull(13) ? null : dataReader.GetString(13);
            return chiTietCaThi;
        }
        public List<ChiTietCaThi> SelectBy_ma_ca_thi(int ma_ca_thi)
        {
            List<ChiTietCaThi> list = new List<ChiTietCaThi>();
            using(IDataReader dataReader = _chiTietCaThiRepository.SelectBy_ma_ca_thi(ma_ca_thi))
            {
                while (dataReader.Read())
                {
                    ChiTietCaThi chiTietCaThi = getProperty(dataReader);
                    list.Add(chiTietCaThi);
                }
                dataReader.Dispose();
            }
            return list;
        }
        public ChiTietCaThi SelectBy_MaCaThi_MaSinhVien(int ma_ca_thi, long ma_sinh_vien)
        {
            ChiTietCaThi chiTietCaThi = new ChiTietCaThi();
            using(IDataReader dataReader = _chiTietCaThiRepository.SelectBy_MaCaThi_MaSinhVien(ma_ca_thi, ma_sinh_vien))
            {
                if (dataReader.Read())
                {
                    chiTietCaThi = getProperty(dataReader);
                }
                dataReader.Dispose();
            }
            return chiTietCaThi;
        }
    }
}
