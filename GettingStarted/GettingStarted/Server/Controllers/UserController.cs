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
    public class UserController : Controller
    {
        private readonly SinhVienService _sinhVienService;
        public UserController(SinhVienService sinhVienService) 
        {
            _sinhVienService = sinhVienService;
        }

        [HttpPost("Verify")]
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
                UpdateLogin(userSession.NavigateSinhVien.MaSinhVien);
                return userSession;
            }
        }
        private void UpdateLogin(long ma_sinh_vien)
        {
            _sinhVienService.Login(ma_sinh_vien, DateTime.Now);
        }
        [HttpPost("UpdateLogout")]
        public ActionResult UpdateLogout([FromQuery] long ma_sinh_vien)
        {
            _sinhVienService.Logout(ma_sinh_vien, DateTime.Now);
            return Ok();
        }
    }
}
