using GettingStarted.Server.BUS;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GettingStarted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : ControllerBase
    {
        private readonly DotThiService _dotThiService;
        private readonly ChiTietDotThiService _chiTietDotThiService;
        private readonly LopAoService _lopAoService;
        private readonly MonHocService _monHocService;
        private readonly CaThiService _caThiService;
        private readonly ChiTietCaThiService _chiTietCaThiService;
        public MonitorController(DotThiService dotThiService, ChiTietDotThiService chiTietDotThiService, LopAoService lopAoService, 
            MonHocService monHocService, CaThiService caThiService, ChiTietCaThiService chiTietCaThiService)
        {
            _dotThiService = dotThiService;
            _chiTietDotThiService = chiTietDotThiService;
            _lopAoService = lopAoService;
            _monHocService = monHocService;
            _caThiService = caThiService;
            _chiTietCaThiService = chiTietCaThiService;
        }
        // GET: api/<MonitorController>
        [HttpGet("GetAllDotThi")]
        public ActionResult<List<DotThi>> GetAllDotThi() 
        {
            return _dotThiService.GetAll();
        }
        [HttpPost("GetMonHoc")]
        public ActionResult<List<MonHoc>> GetMonHoc([FromQuery]int ma_dot_thi)
        {
            List<ChiTietDotThi> chiTietDotThis = _chiTietDotThiService.SelectBy_MaDotThi(ma_dot_thi);
            List<LopAo> lopAos = _lopAoService.SelectBy_ListChiTietDotThi(chiTietDotThis);
            return _monHocService.SelectBy_ListLopAo(lopAos);
        }
        [HttpPost("GetMaPhongThi")]
        public ActionResult<List<LopAo>> GetLopAo([FromQuery] int ma_mon_hoc)
        {
            return _lopAoService.SelectBy_ma_mon_hoc(ma_mon_hoc);
        }
        [HttpPost("GetCaThi")]
        public ActionResult<List<CaThi>> GetCaThi([FromQuery] int ma_dot_thi, [FromQuery] int ma_lop_ao)
        {
            ChiTietDotThi chiTietDotThi = _chiTietDotThiService.SelectBy_MaDotThi_MaLopAo(ma_dot_thi, ma_lop_ao);
            return _caThiService.SelectBy_ma_chi_tiet_dot_thi(chiTietDotThi.MaChiTietDotThi);
        }
        [HttpPost("GetAllChiTietCaThi")]
        public ActionResult<List<ChiTietCaThi>> GetAllChiTietCaThi([FromQuery] int ma_ca_thi)
        {
            return _chiTietCaThiService.SelectBy_ma_ca_thi(ma_ca_thi);
        }
    }
}
