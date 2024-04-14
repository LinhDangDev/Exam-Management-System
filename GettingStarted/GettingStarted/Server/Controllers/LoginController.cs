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
        private readonly SinhVienService _sinhVienService;
        public LoginController(SinhVienService sinhVienService) 
        {
            _sinhVienService = sinhVienService;
        }
        [HttpPost]
        [Route("Verify")]
        // Xác thực sv có trong database, cập nhật sv thời gian sv vào, trả về MSV
        public ActionResult<SinhVien> Verify([FromQuery]string ma_so_sinh_vien)
        {
            // lấy mã sinh viên từ mã số sinh viên
            SinhVien sv = _sinhVienService.SelectBy_ma_so_sinh_vien(ma_so_sinh_vien);
            // cập nhật giờ sinh viên đăng nhập vào hệ thống
            _sinhVienService.Login(sv.MaSinhVien, DateTime.Now);
            return sv;
        }
        [HttpGet]
        [Route("Display")]
        public ActionResult<List<SinhVien>> Display()
        {
            return _sinhVienService.GetAll();
        }
    }
}
