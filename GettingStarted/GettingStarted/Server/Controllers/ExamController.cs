using GettingStarted.Server.BUS;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GettingStarted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : Controller
    {
        private readonly SinhVienService _sinhVienService;
        private readonly ChiTietCaThiService _chiTietCaThiService;
        private readonly DeThiHoanViService _deThiHoanViService;
        private readonly NhomCauHoiService _nhomCauHoiService;
        private readonly ChiTietDeThiHoanViService _chiTietDeThiHoanViService;
        private readonly CauTraLoiService _cauTraLoiService;
        private readonly CauHoiService _cauHoiService;
        private readonly CaThiService _caThiService;
        private readonly NhomCauHoiHoanViService _nhomCauHoiHoanViService;
        public ExamController(SinhVienService sinhVienService, ChiTietCaThiService chiTietCaThiService, DeThiHoanViService deThiHoanViService,
            NhomCauHoiService nhomCauHoiService, ChiTietDeThiHoanViService chiTietDeThiHoanViService, CauTraLoiService cauTraLoiService,
            CauHoiService cauHoiService, CaThiService caThiService, NhomCauHoiHoanViService nhomCauHoiHoanViService)
        {
            _sinhVienService = sinhVienService;
            _chiTietCaThiService = chiTietCaThiService;
            _deThiHoanViService = deThiHoanViService;
            _nhomCauHoiService = nhomCauHoiService;
            _chiTietDeThiHoanViService = chiTietDeThiHoanViService;
            _cauTraLoiService = cauTraLoiService;
            _cauHoiService = cauHoiService;
            _caThiService = caThiService;
            _nhomCauHoiHoanViService = nhomCauHoiHoanViService;
        }
        [HttpPost("GetThongTinSinhVien")]
        public ActionResult<SinhVien> GetThongTinSinhVien([FromQuery] long ma_sinh_vien)
        {
            return _sinhVienService.SelectOne(ma_sinh_vien);
        }
        [HttpPost("GetChiTietCaThiSelectBy_SinhVien")]
        // lấy chi tiết các thông tin của 1 sinh viên thi vào 1 ca giờ cụ thể (đề thi hoán vị)
        public ActionResult<ChiTietCaThi> GetChiTietCaThiSelectBy_SinhVien([FromQuery] int ma_ca_thi, [FromQuery] long ma_sinh_vien)
        {
            return _chiTietCaThiService.SelectBy_MaCaThi_MaSinhVien(ma_ca_thi, ma_sinh_vien);
        }
        [HttpPost("GetThongTinCaThi")]
        public ActionResult<CaThi> GetThongTinCaThi([FromQuery] int ma_ca_thi)
        {
            return _caThiService.SelectOne(ma_ca_thi);
        }
        [HttpPost("HandleAllQuestionExams")]
        public ActionResult<List<TblChiTietDeThiHoanVi>> HandleAllQuestionExams([FromQuery]long ma_de_thi_hoan_vi)
        {
            // lấy mã đề thi gốc
            TblDeThiHoanVi deThiHoanVi = _deThiHoanViService.SelectOne(ma_de_thi_hoan_vi);
            // lấy danh sách các câu hỏi nhóm của đề gốc
            List<TblNhomCauHoi> nhomCauHois = _nhomCauHoiService.SelectBy_MaDeThi(deThiHoanVi.MaDeThi);
            // lấy chi tiết đề thi hoán vị từ mã đề HV và mã nhóm câu hỏi
            List<TblChiTietDeThiHoanVi> result = new List<TblChiTietDeThiHoanVi>();
            foreach(TblNhomCauHoi nhomCauHoi in nhomCauHois)
            {
                List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis = _chiTietDeThiHoanViService.SelectBy_MaDeHV_MaNhom(ma_de_thi_hoan_vi, nhomCauHoi.MaNhom);
                result.AddRange(chiTietDeThiHoanVis);
            }
            return result;
        }
        [HttpPost("GetNoiDungCauHoiNhom")]
        public ActionResult<List<TblNhomCauHoi>> GetNoiDungCauHoiNhom([FromBody] List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis)
        {
            List<TblNhomCauHoi> result = new List<TblNhomCauHoi>();
            foreach(var chiTietDeThiHV in chiTietDeThiHoanVis)
            {
                TblNhomCauHoi nhomCauHoi = _nhomCauHoiService.SelectOne(chiTietDeThiHV.MaNhom);
                if (nhomCauHoi != null && !result.Contains(nhomCauHoi))
                    result.Add(nhomCauHoi);
            }
            return result;
        }
        [HttpPost("GetAllCauTraLoi")]
        public ActionResult<List<TblCauTraLoi>> GetAllCauTraLoi([FromBody] List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis)
        {
            List<TblCauTraLoi> result = new List<TblCauTraLoi>();
            foreach(var chiTietDeThiHV in chiTietDeThiHoanVis)
            {
                List<TblCauTraLoi> cauTraLoi = _cauTraLoiService.SelectBy_MaCauHoi(chiTietDeThiHV.MaCauHoi);
                result.AddRange(cauTraLoi);
            }
            return result;
        }
        [HttpPost("GetNoiDungCauHoi")]
        public ActionResult<List<TblCauHoi>> GetNoiDungCauHoi([FromBody] List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis)
        {
            List<TblCauHoi> result = new List<TblCauHoi>();
            foreach(var chiTietDeThiHV in chiTietDeThiHoanVis)
            {
                TblCauHoi cauHoi = _cauHoiService.SelectOne(chiTietDeThiHV.MaCauHoi);
                result.Add(cauHoi);
            }
            return result;
        }
    }
}
