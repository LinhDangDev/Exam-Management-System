using GettingStarted.Server.Authentication;
using GettingStarted.Server.BUS;
using GettingStarted.Shared;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace GettingStarted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SinhVienService _sinhVienService;
        public LoginController(SinhVienService sinhVienService) 
        {
            _sinhVienService = sinhVienService;
        }
        [HttpPost("Verify")]
        // Xác thực sv có trong database, cập nhật sv thời gian sv vào, trả về MSV
        public ActionResult<UserSession> Verify([FromQuery]string ma_so_sinh_vien)
        {
            //// lấy mã sinh viên từ mã số sinh viên
            //SinhVien sv = _sinhVienService.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien);
            //// cập nhật giờ sinh viên đăng nhập vào hệ thống
            //_sinhVienService.Login(sv.MaSinhVien, DateTime.Now);
            var JwtAuthencationManager = new JwtAuthenticationManager(_sinhVienService);
            var userSession = JwtAuthencationManager.GenerateJwtToken(ma_so_sinh_vien);
            if(userSession is null)
            {
                return Unauthorized();
            }
            else
            {
                return userSession;
            }
        }
        [HttpGet]
        [Route("Display")]
        public ActionResult<List<SinhVien>> Display()
        {
            return _sinhVienService.GetAll();
        }
    }
}
