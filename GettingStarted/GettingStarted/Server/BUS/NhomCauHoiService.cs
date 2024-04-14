using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class NhomCauHoiService
    {
        private readonly INhomCauHoiRepository _nhomCauHoiRepository;
        public NhomCauHoiService(INhomCauHoiRepository nhomCauHoiRepository)
        {
            _nhomCauHoiRepository = nhomCauHoiRepository;   
        }
        private TblNhomCauHoi getProperty(IDataReader dataReader)
        {
            TblNhomCauHoi nhomCauHoi = new TblNhomCauHoi();
            nhomCauHoi.MaNhom = dataReader.GetInt32(0);
            nhomCauHoi.MaDeThi = dataReader.GetInt32(1);
            nhomCauHoi.TenNhom = dataReader.GetString(2);
            nhomCauHoi.NoiDung = dataReader.IsDBNull(3) ? null : dataReader.GetString(3);
            nhomCauHoi.SoCauHoi = dataReader.GetInt32(4);
            nhomCauHoi.HoanVi = dataReader.GetBoolean(5);
            nhomCauHoi.ThuTu = dataReader.GetInt32(6);
            nhomCauHoi.MaNhomCha = dataReader.GetInt32(7);
            nhomCauHoi.SoCauLay = dataReader.GetInt32(8);
            nhomCauHoi.LaCauHoiNhom = dataReader.IsDBNull(9) ? null : dataReader.GetBoolean(9);
            return nhomCauHoi;
        }
        public List<TblNhomCauHoi> SelectBy_MaDeThi(int ma_de_thi)
        {
            List<TblNhomCauHoi> list = new List<TblNhomCauHoi>();
            using(IDataReader dataReader = _nhomCauHoiRepository.SelectBy_MaDeThi(ma_de_thi))
            {
                while (dataReader.Read())
                {
                    TblNhomCauHoi nhomCauHoi = getProperty(dataReader);
                    list.Add(nhomCauHoi);
                }
                dataReader.Dispose();
            }
            return list;
        }
        public TblNhomCauHoi SelectOne(int ma_nhom)
        {
            TblNhomCauHoi nhomCauHoi = new TblNhomCauHoi();
            using(IDataReader dataReader = _nhomCauHoiRepository.SelectOne(ma_nhom))
            {
                if (dataReader.Read())
                {
                    nhomCauHoi = getProperty(dataReader);
                }
                dataReader.Dispose();
            }
            return nhomCauHoi;
        }
    }
}
