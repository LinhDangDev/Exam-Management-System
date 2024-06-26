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
        private TblChiTietDeThiHoanVi getProperty(IDataReader dataReader, TblNhomCauHoiHoanVi nhomCauHoiHoanVi)
        {
            TblChiTietDeThiHoanVi chiTietDeThiHoanVi = new TblChiTietDeThiHoanVi();
            chiTietDeThiHoanVi.MaDeHv = dataReader.GetInt64(0);
            chiTietDeThiHoanVi.MaNhom = dataReader.GetInt32(1);
            chiTietDeThiHoanVi.MaCauHoi = dataReader.GetInt32(2);
            chiTietDeThiHoanVi.ThuTu = dataReader.GetInt32(3);
            chiTietDeThiHoanVi.HoanViTraLoi = dataReader.IsDBNull(4) ? null : dataReader.GetString(4);
            chiTietDeThiHoanVi.DapAn = dataReader.IsDBNull(5) ? null : dataReader.GetInt32(5);
            // đây là trường đặc biệt có tên "ma" - là đối tượng nhóm câu hỏi hoán vị
            chiTietDeThiHoanVi.Ma = nhomCauHoiHoanVi;
            return chiTietDeThiHoanVi;
        }
        private TblNhomCauHoiHoanVi getPropertyNhomCauHoi(IDataReader dataReader)
        {
            TblNhomCauHoiHoanVi nhomCauHoiHoanVi = new TblNhomCauHoiHoanVi();
            nhomCauHoiHoanVi.MaDeHv = dataReader.GetInt64(0);
            nhomCauHoiHoanVi.MaNhom = dataReader.GetInt32(1);
            nhomCauHoiHoanVi.ThuTu = dataReader.GetInt32(2);
            return nhomCauHoiHoanVi;
        }
        public List<TblChiTietDeThiHoanVi> SelectBy_MaDeHV_MaNhom(long ma_de_hoan_vi, int ma_nhom)
        {
            List<TblChiTietDeThiHoanVi> list = new List<TblChiTietDeThiHoanVi>();
            // vì chi Tiết đề thi có 1 trường đặc biệt là 1 đối tượng nhomCauHoiHoanVi
            TblNhomCauHoiHoanVi nhomCauHoiHoanVi = _nhomCauHoiHoanViService.SelectOne(ma_de_hoan_vi, ma_nhom);
            using(IDataReader dataReader = _chiTietDeThiHoanViRepository.SelectBy_MaDeHV_MaNhom(ma_de_hoan_vi, ma_nhom))
            {
                while (dataReader.Read())
                {
                    TblChiTietDeThiHoanVi chiTietDeThiHoanVi = getProperty(dataReader, nhomCauHoiHoanVi);
                    //vẫn chưa thấu cảm với thuộc tính "ma"
                    list.Add(chiTietDeThiHoanVi);
                }
                dataReader.Dispose();
            }
            return list;
        }
    }
}
