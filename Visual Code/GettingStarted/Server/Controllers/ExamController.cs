using GettingStarted.Server.BUS;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GettingStarted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : Controller
    {
        SinhVienService _sinhVienService;
        public ExamController(SinhVienService sinhVienService)
        {
            _sinhVienService = sinhVienService;
        }

        [HttpPost]
        [Route("/GetThongTinSV")]
        public ActionResult<SinhVien> GetThongTinSV([FromBody]long ma_sinh_vien)
        {
            return _sinhVienService.GetSinhVien_FromMaSoSV(ma_sinh_vien);
        }
    }
}
