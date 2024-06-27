using GettingStarted.Shared.Models;

namespace GettingStarted.Server.BUS
{
    public class CustomDeThiService
    {
        private readonly CustomDeThi _customDeThi;
        private readonly ChiTietDeThiHoanViService _chiTietDeThiHoanViService;
        private readonly NhomCauHoiService _nhomCauHoiService;
        private readonly CauHoiService _cauHoiService;
        private readonly CauTraLoiService _cauTraLoiService;

        public CustomDeThiService(CustomDeThi customDeThi, ChiTietDeThiHoanViService chiTietDeThiHoanViService, NhomCauHoiService nhomCauHoiService, CauHoiService cauHoiService, CauTraLoiService cauTraLoiService)
        {
            _customDeThi = customDeThi;
            _chiTietDeThiHoanViService = chiTietDeThiHoanViService;
            _nhomCauHoiService = nhomCauHoiService;
            _cauHoiService = cauHoiService;
            _cauTraLoiService = cauTraLoiService;
        }       
        public List<CustomDeThi> handleDeThi(long ma_de_hoan_vi)
        { 
            List<CustomDeThi> result = new List<CustomDeThi>();
            List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis = getNoiDungChiTietDeThiHV(ma_de_hoan_vi);
            foreach (var item in chiTietDeThiHoanVis)
                result.Add(getNoiDungFromCTDeThiHV(item));
            return result;
        }
        private List<TblChiTietDeThiHoanVi> getNoiDungChiTietDeThiHV(long ma_de_hoan_vi)
        {
            return _chiTietDeThiHoanViService.SelectBy_MaDeHV(ma_de_hoan_vi); ;
        }
        private TblNhomCauHoi getNoiDungCauHoiNhom(int ma_cau_hoi_nhom)
        {
            return _nhomCauHoiService.SelectOne(ma_cau_hoi_nhom);
        }
        private TblCauHoi getNoiDungCauHoi(int ma_cau_hoi)
        {
            return _cauHoiService.SelectOne(ma_cau_hoi);
        }
        private List<TblCauTraLoi> getNoiDungCauTraLoi(int ma_cau_hoi)
        {
            return _cauTraLoiService.SelectBy_MaCauHoi(ma_cau_hoi);
        }
        private CustomDeThi getNoiDungFromCTDeThiHV(TblChiTietDeThiHoanVi chiTietDeThiHoanVi)
        {
            CustomDeThi chiTietDeThi = new CustomDeThi();
            TblNhomCauHoi nhomCauHoi = getNoiDungCauHoiNhom(chiTietDeThiHoanVi.MaNhom);
            TblCauHoi cauHoi = getNoiDungCauHoi(chiTietDeThiHoanVi.MaCauHoi);
            List<TblCauTraLoi> cauTraLois = getNoiDungCauTraLoi(chiTietDeThiHoanVi.MaCauHoi);

            chiTietDeThi.MaNhom = nhomCauHoi.MaNhom;
            chiTietDeThi.MaCauHoi = cauHoi.MaCauHoi;

            // lấy nội dung của mã nhóm cha (nếu có)
            if (nhomCauHoi.MaNhomCha != 1)
                chiTietDeThi.NoiDungCauHoiNhomCha = getNoiDungCauHoiNhom(nhomCauHoi.MaNhomCha).NoiDung;
            chiTietDeThi.NoiDungCauHoiNhom = nhomCauHoi.NoiDung;
            chiTietDeThi.NoiDungCauHoi = cauHoi.NoiDung;
            chiTietDeThi.KieuNoiDungCauHoi = cauHoi.KieuNoiDung;
            chiTietDeThi.CauTraLois = new Dictionary<int, string?>();

            // lấy nội dung câu hỏi bằng dictionary
            chiTietDeThi.CauTraLois = new Dictionary<int, string?>();
            foreach (var item in cauTraLois)
                chiTietDeThi.CauTraLois.Add(item.MaCauTraLoi, item.NoiDung);
            return chiTietDeThi;
        }
    }
}
