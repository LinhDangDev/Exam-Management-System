using GettingStarted.Server.Authentication;
using GettingStarted.Server.BUS;
using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GettingStarted.Server.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly CaThiService _caThiService;
        private readonly ChiTietDotThiService _chiTietDotThiService;
        private readonly DotThiService _dotThiService;
        private readonly LopAoService _lopAoService;
        private readonly MonHocService _monHocService;
        private readonly ChiTietCaThiService _chiTietCaThiService;
        private readonly SinhVienService _sinhVienService;
        public AdminController(UserService userService, CaThiService caThiService, ChiTietDotThiService chiTietDotThiService, DotThiService dotThiService,
            LopAoService lopAoService, MonHocService monHocService, ChiTietCaThiService chiTietCaThiService, SinhVienService sinhVienService)
        {
            _userService = userService;
            _caThiService = caThiService;
            _chiTietDotThiService = chiTietDotThiService;
            _dotThiService = dotThiService;
            _lopAoService = lopAoService;
            _monHocService = monHocService;
            _chiTietCaThiService = chiTietCaThiService;
            _sinhVienService = sinhVienService;
        }
        //API Manager & Control
        [HttpPost("Verify")]
        [AllowAnonymous]
        public ActionResult<UserSession> Verify([FromQuery] string loginName, [FromQuery] string password)
        {
            var JwtAuthencationManager = new JwtAuthenticationManager(_userService);
            var userSession = JwtAuthencationManager.GenerateJwtToken(loginName, password);
            if (userSession == null)
            {
                return Unauthorized();
            }
            else
            {
                //UpdateLogin(string username, string password);
                return userSession;
            }
        }
        [HttpPost("getThongTinUser")]
        [AllowAnonymous]
        public ActionResult<User> getThongTinUser([FromQuery] string loginName, [FromQuery] string password)
        {
            return _userService.SelectByLoginName(loginName);
        }
        [HttpPost("UpdateTinhTrangCaThi")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<CaThi>> UpdateTinhTrangCaThi([FromQuery] int ma_ca_thi, [FromQuery] bool isActived)
        {
            _caThiService.ca_thi_Activate(ma_ca_thi, isActived);
            return GetAllCaThi();
        }
        [HttpPost("DungCaThi")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<CaThi>> DungCaThi([FromQuery] int ma_ca_thi)
        {
            _caThiService.ca_thi_Ketthuc(ma_ca_thi);
            return GetAllCaThi();
        }
        [HttpGet("GetAllCaThi")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<CaThi>> GetAllCaThi()
        {
            List<CaThi> result = _caThiService.ca_thi_GetAll();
            foreach (var item in result)
                item.MaChiTietDotThiNavigation = getThongTinChiTietDotThi(item.MaChiTietDotThi);
            return result;
        }
        //API Monitor
        [HttpPost("GetThongTinCTCaThiTheoMaCaThi")]
        [Authorize(Roles ="Admin")]
        public ActionResult<List<ChiTietCaThi>> GetThongTinCTCaThiTheoMaCaThi([FromQuery] int ma_ca_thi)
        {
            List<ChiTietCaThi> result = _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
            foreach (var item in result)
                if(item.MaSinhVien != null)
                    item.MaSinhVienNavigation = getThongTinSinhVien((long)item.MaSinhVien);
            return result;
        }
        [HttpPost("UpdateLogoutSinhVien")]
        public ActionResult UpdateLogoutSinhVien(long ma_sinh_vien)
        {
            _sinhVienService.Logout(ma_sinh_vien, DateTime.Now);
            return Ok();
        }
        [HttpPost("CongGioSinhVien")]
        [Authorize(Roles = "Admin")]
        public ActionResult CongGioSinhVien([FromBody]ChiTietCaThi chiTietCaThi)
        {
            _chiTietCaThiService.CongGio(chiTietCaThi);
            return Ok();
        }
        private SinhVien getThongTinSinhVien(long ma_sinh_vien)
        {
            return _sinhVienService.SelectOne(ma_sinh_vien);
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
            if (lopAo.MaMonHoc != null) // ma_mon_hoc có thể bị null ở đây
                lopAo.MaMonHocNavigation = getThongTinMonHoc((int)lopAo.MaMonHoc);
            return _lopAoService.SelectOne(ma_lop_ao);
        }
        private MonHoc getThongTinMonHoc(int ma_mon_hoc)
        {
            return _monHocService.SelectOne(ma_mon_hoc);
        }
    }
}
