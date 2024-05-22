using GettingStarted.Server.BUS;
using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GettingStarted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly CaThiService _caThiService;
        private readonly ChiTietDotThiService _chiTietDotThiService;
        private readonly DotThiService _dotThiService;
        private readonly LopAoService _lopAoService;
        private readonly MonHocService _monHocService;
        public AdminController(CaThiService caThiService, ChiTietDotThiService chiTietDotThiService, DotThiService dotThiService, LopAoService lopAoService,
                                MonHocService monHocService) {
            _caThiService = caThiService;
            _chiTietDotThiService = chiTietDotThiService;
            _dotThiService = dotThiService;
            _lopAoService = lopAoService;
            _monHocService = monHocService;
        }
        [HttpPost("UpdateTinhTrangCaThi")]
        public ActionResult<List<CaThi>> UpdateTinhTrangCaThi([FromQuery] int ma_ca_thi, [FromQuery] bool isActived)
        {
            _caThiService.ca_thi_Activate(ma_ca_thi, isActived);
            return GetAllCaThi();
        }
        [HttpPost("DungCaThi")]
        public ActionResult<List<CaThi>> DungCaThi([FromQuery] int ma_ca_thi)
        {
            _caThiService.ca_thi_Ketthuc(ma_ca_thi);
            return GetAllCaThi();
        }
        [HttpGet("GetAllCaThi")]
        public ActionResult<List<CaThi>> GetAllCaThi()
        {
            List<CaThi> result = _caThiService.ca_thi_GetAll();
            foreach (var item in result)
                item.MaChiTietDotThiNavigation = getThongTinChiTietDotThi(item.MaChiTietDotThi);
            return result;
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
            if(lopAo.MaMonHoc != null) // ma_mon_hoc có thể bị null ở đây
                lopAo.MaMonHocNavigation = getThongTinMonHoc((int)lopAo.MaMonHoc);
            return _lopAoService.SelectOne(ma_lop_ao);
        }
        private MonHoc getThongTinMonHoc(int ma_mon_hoc)
        {
            return _monHocService.SelectOne(ma_mon_hoc);
        }
    }
}
