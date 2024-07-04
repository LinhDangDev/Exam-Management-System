using GettingStarted.Server.BUS;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GettingStarted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InfoController : ControllerBase
    {
        private const int SO_PHUT_LECH_CA_THI = 2880; // thí sinh nhận được ca thi chênh lệch 30 phút
        private readonly SinhVienService _sinhVienService;
        private readonly ChiTietCaThiService _chiTietCaThiService;
        private readonly CaThiService _caThiService;
        private readonly ChiTietDotThiService _chiTietDotThiService;
        private readonly LopAoService _lopAoService;
        private readonly MonHocService _monHocService;
        private readonly DotThiService _dotThiService;
        public InfoController(SinhVienService sinhVienService, ChiTietCaThiService chiTietCaThiService,
            CaThiService caThiService, ChiTietDotThiService chiTietDotThiService, LopAoService lopAoService, MonHocService monHocService, DotThiService dotThiService)
        {
            _sinhVienService = sinhVienService;
            _chiTietCaThiService = chiTietCaThiService;
            _caThiService = caThiService;
            _chiTietDotThiService = chiTietDotThiService;
            _lopAoService = lopAoService;
            _monHocService = monHocService;
            _dotThiService = dotThiService;
        }
        [HttpPost("GetThongTinChiTietCaThi")]
        // lấy chi tiết các thông tin của 1 sinh viên thi vào 1 ca giờ cụ thể (đề thi hoán vị)
        public ActionResult<ChiTietCaThi>? GetThongTinChiTietCaThi([FromQuery] long ma_sinh_vien)
        {
            CaThi? caThi = getThongTinCaThi(ma_sinh_vien);
            if(caThi != null)
            {
                ChiTietCaThi chiTietCaThi = _chiTietCaThiService.SelectBy_MaCaThi_MaSinhVien(caThi.MaCaThi, ma_sinh_vien);
                chiTietCaThi.MaSinhVienNavigation = getThongTinSV(ma_sinh_vien);
                chiTietCaThi.MaCaThiNavigation = getThongTinCaThi(ma_sinh_vien);
                return chiTietCaThi;
            }
            ChiTietCaThi newChiTietCaThi = new ChiTietCaThi();
            newChiTietCaThi.MaSinhVienNavigation = getThongTinSV(ma_sinh_vien);
            return newChiTietCaThi;
        }
        private SinhVien? getThongTinSV(long ma_sinh_vien)
        {
            return _sinhVienService.SelectOne(ma_sinh_vien);
        }
        private CaThi? getThongTinCaThi(long ma_sinh_vien)
        {
            // lấy 1 list danh sách ca thi của 1 sinh viên
            List<ChiTietCaThi> chiTietCaThis = _chiTietCaThiService.SelectBy_ma_sinh_vien(ma_sinh_vien);
            List<CaThi> caThis = new List<CaThi>();
            foreach(var chiTietCaThi in chiTietCaThis)
            {
                CaThi? caThi = (chiTietCaThi.MaCaThi != null) ? _caThiService.SelectOne((int)chiTietCaThi.MaCaThi) : null;
                if (caThi != null && !caThis.Contains(caThi))
                {
                    caThis.Add(caThi);
                }
            }
            // chỉ lấy ra duy nhất cho 1 ca thi gần đến thời gian thi
            DateTime currentTime = DateTime.Now;
            int chenh_lech_phut = SO_PHUT_LECH_CA_THI; // có thể thay đổi tùy theo nhu cầu
            DateTime gio_tren = currentTime.AddMinutes(chenh_lech_phut);
            DateTime gio_duoi = currentTime.AddMinutes(-chenh_lech_phut);
            CaThi? result = caThis.FirstOrDefault(p => p.ThoiGianBatDau >= gio_duoi && p.ThoiGianBatDau <= gio_tren);
            if(result != null)
                result.MaChiTietDotThiNavigation = getThongTinChiTietDotThi(result.MaChiTietDotThi);
            return result ?? null;
        }
        private ChiTietDotThi getThongTinChiTietDotThi(int ma_chi_tiet_dot_thi)
        {
            ChiTietDotThi chiTietDotThi = _chiTietDotThiService.SelectOne(ma_chi_tiet_dot_thi);
            chiTietDotThi.MaDotThiNavigation = getThongTinDotThi(chiTietDotThi.MaDotThi);
            chiTietDotThi.MaLopAoNavigation = getThongTinLopAo(chiTietDotThi.MaLopAo);
            return chiTietDotThi;
        }
        private DotThi getThongTinDotThi(int ma_dot_thi)
        {
            return _dotThiService.SelectOne(ma_dot_thi);
        }
        private LopAo getThongTinLopAo(int ma_lop_ao)
        {
            LopAo lopAo = _lopAoService.SelectOne(ma_lop_ao);
            lopAo.MaMonHocNavigation = getThongTinMonHoc(ma_lop_ao);
            return lopAo;
        }
        private MonHoc getThongTinMonHoc(int ma_mon_hoc)
        {
            return _monHocService.SelectOne(ma_mon_hoc);
        }
        [HttpPost("UpdateBatDauThi")]
        public ActionResult UpdateBatDauThi([FromBody] ChiTietCaThi chiTietCaThi)
        {
            _chiTietCaThiService.UpdateBatDau(chiTietCaThi);
            return Ok();
        }
    }
}
