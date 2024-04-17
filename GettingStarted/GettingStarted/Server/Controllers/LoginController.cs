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
    public class LoginController : Controller
    {
        private readonly SinhVienService _sinhVienService;
        public LoginController(SinhVienService sinhVienService) 
        {
            _sinhVienService = sinhVienService;
        }
        [HttpPost("Check")]
        [AllowAnonymous]
        public ActionResult<SinhVien> Check([FromQuery]string ma_so_sinh_vien)
        {
            SinhVien sv = _sinhVienService.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien);
            return sv;
        }

        [HttpPost("Verify")]
        [AllowAnonymous]
        // Xác thực sv có trong database, cập nhật sv thời gian sv vào, trả về MSV
        public ActionResult<UserSession> Verify([FromQuery]string ma_so_sinh_vien)
        {
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
        [HttpPost("UpdateLogin")]
        public ActionResult UpdateLogin([FromQuery]long ma_sinh_vien, [FromQuery] DateTime last_log_in)
        {
            _sinhVienService.Login(ma_sinh_vien, last_log_in);
            return Ok();
        }
        [HttpPost("UpdateLogout")]
        public ActionResult UpdateLogout([FromQuery] long ma_sinh_vien, [FromQuery] DateTime last_log_out)
        {
            _sinhVienService.Logout(ma_sinh_vien, last_log_out);
            return Ok();
        }
    }
}
