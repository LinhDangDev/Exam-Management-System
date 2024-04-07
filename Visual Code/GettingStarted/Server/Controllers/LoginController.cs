using GettingStarted.Server.BUS;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace GettingStarted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        SinhVienService _sinhVienService;
        public LoginController(SinhVienService sinhVienService) 
        {
            _sinhVienService = sinhVienService;
        }
        [HttpPost]
        [Route("Verify")]
        // Xác thực sv có trong database, cập nhật sv thời gian sv vào, trả về MSV
        public ActionResult<long> Verify([FromBody]string ma_so_sinh_vien)
        {
            // lấy mã sinh viên từ mã số sinh viên
            long ma_sinh_vien = _sinhVienService.GetMaSV_FormMSSV(ma_so_sinh_vien);
            // cập nhật giờ sinh viên đăng nhập vào hệ thống
            _sinhVienService.LogIn(ma_sinh_vien, DateTime.Now);
            return Ok(ma_sinh_vien);
        }
        [HttpGet]
        [Route("Display")]
        public ActionResult<List<SinhVien>> Display()
        {
            return _sinhVienService.GetAllSinhVien();
        }
    }
}
