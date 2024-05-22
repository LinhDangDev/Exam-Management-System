using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class ChiTietDeThiHoanViService
    {
        private readonly IChiTietDeThiHoanViRepository _chiTietDeThiHoanViRepository;
        private readonly NhomCauHoiHoanViService _nhomCauHoiHoanViService;
        public ChiTietDeThiHoanViService(IChiTietDeThiHoanViRepository chiTietDeThiHoanViRepository, NhomCauHoiHoanViService nhomCauHoiHoanViService)
        {
            _chiTietDeThiHoanViRepository = chiTietDeThiHoanViRepository;
            _nhomCauHoiHoanViService = nhomCauHoiHoanViService;
        }
        private TblChiTietDeThiHoanVi getProperty(IDataReader dataReader)
        {
            TblChiTietDeThiHoanVi chiTietDeThiHoanVi = new TblChiTietDeThiHoanVi();
            chiTietDeThiHoanVi.MaDeHv = dataReader.GetInt64(0);
            chiTietDeThiHoanVi.MaNhom = dataReader.GetInt32(1);
            chiTietDeThiHoanVi.MaCauHoi = dataReader.GetInt32(2);
            chiTietDeThiHoanVi.ThuTu = dataReader.GetInt32(3);
            chiTietDeThiHoanVi.HoanViTraLoi = dataReader.IsDBNull(4) ? null : dataReader.GetString(4);
            chiTietDeThiHoanVi.DapAn = dataReader.IsDBNull(5) ? null : dataReader.GetInt32(5);
            return chiTietDeThiHoanVi;
        }
        public List<TblChiTietDeThiHoanVi> SelectBy_MaDeHV(long maDeHV)
        {
            List<TblChiTietDeThiHoanVi> result = new List<TblChiTietDeThiHoanVi>();
            using(IDataReader dataReader = _chiTietDeThiHoanViRepository.SelectBy_MaDeHV(maDeHV))
            {
                while (dataReader.Read())
                {
                    TblChiTietDeThiHoanVi chiTietDeThiHoanVi = getProperty(dataReader);
                    result.Add(chiTietDeThiHoanVi);
                }
            }
            return result;
        }
        public List<TblChiTietDeThiHoanVi> SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom)
        {
            List<TblChiTietDeThiHoanVi> list = new List<TblChiTietDeThiHoanVi>();
            using (IDataReader dataReader = _chiTietDeThiHoanViRepository.SelectBy_MaDeHV_MaNhom(ma_de_hoan_vi, ma_nhom))
            {
                while (dataReader.Read())
                {
                    TblChiTietDeThiHoanVi chiTietDeThiHoanVi = getProperty(dataReader);
                    //vẫn chưa thấu cảm với thuộc tính "ma"
                    list.Add(chiTietDeThiHoanVi);
                }
            }
            return list;
        }
    }
}
