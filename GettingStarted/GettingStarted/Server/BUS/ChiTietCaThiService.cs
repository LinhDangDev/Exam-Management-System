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
        public ChiTietCaThi SelectOne(int chi_tiet_ca_thi)
        {
            ChiTietCaThi chiTietCaThi = new ChiTietCaThi();
            using (IDataReader dataReader = _chiTietCaThiRepository.SelectOne(chi_tiet_ca_thi))
            {
                if (dataReader.Read())
                {
                    chiTietCaThi = getProperty(dataReader);
                }
            }
            return chiTietCaThi;
        }
        public List<ChiTietCaThi> SelectBy_ma_ca_thi(int ma_ca_thi)
        {
            List<ChiTietCaThi> result = new List<ChiTietCaThi>();
            using(IDataReader dataReader = _chiTietCaThiRepository.SelectBy_ma_ca_thi(ma_ca_thi))
            {
                while (dataReader.Read())
                {
                    result.Add(getProperty(dataReader));
                }
            }
            return result;
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
            }
            return chiTietCaThi;
        }
        public List<ChiTietCaThi> SelectBy_ma_sinh_vien(long ma_sinh_vien)
        {
            List<ChiTietCaThi> result = new List<ChiTietCaThi>();
            using(IDataReader dataReader = _chiTietCaThiRepository.SelectBy_ma_sinh_vien(ma_sinh_vien))
            {
                while (dataReader.Read())
                {
                    result.Add(getProperty(dataReader));
                }
            }
            return result;
        }
        public void UpdateBatDau(ChiTietCaThi chiTietCaThi)
        {
            int ma_chi_tiet_ca_thi = chiTietCaThi.MaChiTietCaThi;
            DateTime? thoi_gian_bat_dau = chiTietCaThi.ThoiGianBatDau;
            if(!_chiTietCaThiRepository.UpdateBatDau(ma_chi_tiet_ca_thi, thoi_gian_bat_dau))
            {
                throw new Exception("Can't UpdateBatDau table chi_tiet_ca_thi");
            }
        }
        public void UpdateKetThuc(ChiTietCaThi chiTietCaThi)
        {
            //float diem, int so_cau_dung, int tong_so_cau
            int ma_chi_tiet_ca_thi = chiTietCaThi.MaChiTietCaThi;
            DateTime? thoi_gian_ket_thuc = chiTietCaThi.ThoiGianKetThuc;
            double diem = chiTietCaThi.Diem;
            int? so_cau_dung = chiTietCaThi.SoCauDung;
            int? tong_so_cau = chiTietCaThi.TongSoCau;
            if (!_chiTietCaThiRepository.UpdateKetThuc(ma_chi_tiet_ca_thi, thoi_gian_ket_thuc, diem, so_cau_dung, tong_so_cau))
            {
                throw new Exception("Can't UpdateKetThuc table chi_tiet_ca_thi");
            }
        }
    }
}
