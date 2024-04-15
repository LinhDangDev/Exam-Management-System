using GettingStarted.Server.BUS;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GettingStarted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly SinhVienService _sinhVienService;
        private readonly ChiTietCaThiService _chiTietCaThiService;
        private readonly CaThiService _caThiService;
        private readonly ChiTietDotThiService _chiTietDotThiService;
        private readonly LopAoService _lopAoService;
        private readonly MonHocService _monHocService;
        public InfoController(SinhVienService sinhVienService, ChiTietCaThiService chiTietCaThiService,
            CaThiService caThiService, ChiTietDotThiService chiTietDotThiService, LopAoService lopAoService, MonHocService monHocService)
        {
            _sinhVienService = sinhVienService;
            _chiTietCaThiService = chiTietCaThiService;
            _caThiService = caThiService;
            _chiTietDotThiService = chiTietDotThiService;
            _lopAoService = lopAoService;
            _monHocService = monHocService;
        }
        [HttpPost("GetThongTinSinhVien")]
        public ActionResult<SinhVien> GetThongTinSinhVien([FromQuery] long ma_sinh_vien)
        {
            return _sinhVienService.SelectOne(ma_sinh_vien);
        }
        [HttpPost("GetThongTinSinhVienTuMSSV")]
        public ActionResult<SinhVien> GetThongTinSinhVienTuMSSV([FromQuery] string ma_so_sinh_vien)
        {
            return _sinhVienService.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien);
        }
        [HttpPost("GetThongTinCaThi")]
        public ActionResult<CaThi?> GetThongTinCaThi([FromQuery] long ma_sinh_vien)
        {
            // lấy 1 list danh sách ca thi của 1 sinh viên
            List<ChiTietCaThi> chiTietCaThis = _chiTietCaThiService.SelectBy_ma_sinh_vien(ma_sinh_vien);
            DateTime currentTime = DateTime.Now;
            int chenh_lech_phut = 32000; // có thể thay đổi tùy theo nhu cầu
            DateTime gio_tren = currentTime.AddMinutes(chenh_lech_phut);
            DateTime gio_duoi = currentTime.AddMinutes(-chenh_lech_phut);
            List<CaThi> caThis = new List<CaThi>();
            foreach(var chiTietCaThi in chiTietCaThis)
            {
                CaThi caThi = _caThiService.SelectOne((int)chiTietCaThi.MaCaThi);
                if (!caThis.Contains(caThi))
                {
                    caThis.Add(caThi);
                }
            }
            // chỉ lấy ra duy nhất cho 1 ca thi gần đến thời gian thi
            CaThi? result = caThis.FirstOrDefault(p => p.ThoiGianBatDau >= gio_duoi && p.ThoiGianBatDau <= gio_tren);
            return result;
        }
        [HttpPost("GetThongTinMonThi")]
        public ActionResult<MonHoc> GetThongTinMonThi([FromQuery] int ma_ca_thi)
        {
            CaThi caThi = _caThiService.SelectOne(ma_ca_thi);
            ChiTietDotThi chiTietDotThi = _chiTietDotThiService.SelectOne(caThi.MaChiTietDotThi);
            LopAo lopAo = _lopAoService.SelectOne(chiTietDotThi.MaLopAo);
            return _monHocService.SelectOne((int)lopAo.MaMonHoc);
        }
    }
}
