using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;
using System.Diagnostics;

namespace GettingStarted.Server.BUS
{
    public class SinhVienService
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly IChiTietCaThiRepository _chiTietCaThiRepository;
        public SinhVienService(ISinhVienRepository sinhVienRepository, IChiTietCaThiRepository chiTietCaThiRepository)
        {
            _sinhVienRepository = sinhVienRepository;
            _chiTietCaThiRepository = chiTietCaThiRepository;
        }
        // lấy mã đề thi hoán vị từ bảng chi tiết ca thi
        public int GetMaDeHV_FromChiTietCaThi(long ma_sinh_vien)
        {
            IDataReader dataReader = _chiTietCaThiRepository.GetBy_MaSinhVien(ma_sinh_vien);
            if(dataReader.Read())
            {
                return dataReader.GetInt32(3);
            }
            dataReader.Close();
            return -1;
        }
        public List<SinhVien> GetAllSinhVien()
        {
            List<SinhVien> list = new List<SinhVien>();
            using(IDataReader dataReader = _sinhVienRepository.GetAllSinhVien())
            {
                while (dataReader.Read())
                {
                    SinhVien sv = new SinhVien();
                    sv.MaSinhVien = dataReader.GetInt64(0);
                    list.Add(sv);
                }
            }
            return list;
        }
        // kiểm tra xem sv có tồn tại trong database hay không
        public bool SinhVien_Exsist(string ma_so_sinh_vien)
        {
            var result = _sinhVienRepository.GetMaSV_FormMSSV(ma_so_sinh_vien);
            return result != null;
        }
        public long GetMaSV_FormMSSV(string ma_so_sinh_vien)
        {
            long ma_sinh_vien = (long)_sinhVienRepository.GetMaSV_FormMSSV(ma_so_sinh_vien);
            return ma_sinh_vien;
        }
        public void LogIn(long ma_sinh_vien, DateTime last_log_in)
        {
            var result = _sinhVienRepository.LogIn(ma_sinh_vien, last_log_in);
        }
        //lấy thông tin của 1 sinh viên từ mã số sinh viên
        public SinhVien GetSinhVien_FromMaSoSV(long ma_sinh_vien)
        {
            SinhVien sv = new SinhVien();
            using(IDataReader dataReader = _sinhVienRepository.GetSinhVien(ma_sinh_vien))
            {
                if(dataReader.Read())
                {
                    sv.HoVaTenLot = dataReader.GetString(1);
                    sv.TenSinhVien = dataReader.GetString(2);
                    sv.GioiTinh = dataReader.GetInt16(3);
                    sv.NgaySinh = dataReader.GetDateTime(4);
                    sv.MaLop = dataReader.GetInt32(5);
                    sv.DiaChi = dataReader.GetString(6);
                    sv.Email = dataReader.GetString(7);
                    sv.DienThoai = dataReader.GetString(8);
                    sv.MaSoSinhVien = dataReader.GetString(9);
                    sv.StudentId = dataReader.GetGuid(10);
                    sv.IsLoggedIn = dataReader.GetBoolean(11);
                    sv.LastLoggedOut = dataReader.GetDateTime(12);
                    sv.LastLoggedOut = dataReader.GetDateTime(13);
                    sv.Photo = null; // chưa biết cách xử lí (image === byte)
                }
            }
            return sv;
        }
    }
}
