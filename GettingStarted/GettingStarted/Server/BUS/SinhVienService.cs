using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;
using System.Diagnostics;

namespace GettingStarted.Server.BUS
{
    public class SinhVienService
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        public SinhVienService(ISinhVienRepository sinhVienRepository)
        {
            _sinhVienRepository = sinhVienRepository;
        }
        public List<SinhVien> GetAll()
        {
            List<SinhVien> list = new List<SinhVien>();
            using(IDataReader dataReader = _sinhVienRepository.GetAll())
            {
                while (dataReader.Read())
                {
                    SinhVien sv = getProperty(dataReader);
                    list.Add(sv);
                }
                dataReader.Dispose();
            }
            return list;
        }
        private SinhVien getProperty(IDataReader dataReader)
        {
            SinhVien sv = new SinhVien();
            sv.MaSinhVien = dataReader.GetInt64(0);
            sv.HoVaTenLot = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
            sv.TenSinhVien = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);
            sv.GioiTinh = dataReader.IsDBNull(3) ? null : dataReader.GetInt16(3);
            sv.NgaySinh = dataReader.IsDBNull(4) ? null : dataReader.GetDateTime(4);
            sv.MaLop = dataReader.IsDBNull(5) ? null : dataReader.GetInt32(5);
            sv.DiaChi = dataReader.IsDBNull(6) ? null : dataReader.GetString(6);
            sv.Email = dataReader.IsDBNull(7) ? null : dataReader.GetString(7);
            sv.DienThoai = dataReader.IsDBNull(8) ? null : dataReader.GetString(8);
            sv.MaSoSinhVien = dataReader.IsDBNull(9) ? null : dataReader.GetString(9);
            sv.StudentId = dataReader.IsDBNull(10) ? null : dataReader.GetGuid(10);
            sv.IsLoggedIn = dataReader.IsDBNull(11) ? null : dataReader.GetBoolean(11);
            sv.LastLoggedOut = dataReader.IsDBNull(12) ? null : dataReader.GetDateTime(12);
            sv.LastLoggedOut = dataReader.IsDBNull(13) ? null : dataReader.GetDateTime(13);
            sv.Photo = null; // chưa biết cách xử lí (image === byte)
            return sv;
        }
        public SinhVien SelectBy_ma_so_sinh_vien(string ma_so_sinh_vien)
        {
            SinhVien sv = new SinhVien();
            using (IDataReader dataReader = _sinhVienRepository.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien))
            {
                if (dataReader.Read())
                {
                    sv = getProperty(dataReader);
                }
                dataReader.Dispose();
            }
            return sv;
        }
        public void Login(long ma_sinh_vien, DateTime last_log_in)
        {
            _sinhVienRepository.Login(ma_sinh_vien, last_log_in);
        }
        //lấy thông tin của 1 sinh viên từ mã số sinh viên
        public SinhVien SelectOne(long ma_sinh_vien)
        {
            SinhVien sv = new SinhVien();
            using(IDataReader dataReader = _sinhVienRepository.SelectOne(ma_sinh_vien))
            {
                if(dataReader.Read())
                {
                    sv = getProperty(dataReader);
                }
                dataReader.Dispose();
            }
            return sv;
        }
    }
}
