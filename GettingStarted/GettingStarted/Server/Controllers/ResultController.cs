using Microsoft.AspNetCore.Mvc;
using GettingStarted.Server.BUS;
using GettingStarted.Shared.Models;


namespace GettingStarted.Server.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : Controller
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
        private readonly MonHoc _MonHocServices;
        public ResultController(SinhVienService sinhVienService, ChiTietCaThiService chiTietCaThiService, DeThiHoanViService deThiHoanViService,
            NhomCauHoiService nhomCauHoiService, ChiTietDeThiHoanViService chiTietDeThiHoanViService, CauTraLoiService cauTraLoiService,
            CauHoiService cauHoiService, CaThiService caThiService, NhomCauHoiHoanViService nhomCauHoiHoanViService,MonHoc monHocServices)
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
            _MonHocServices = monHocServices;
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
        
        
    }

