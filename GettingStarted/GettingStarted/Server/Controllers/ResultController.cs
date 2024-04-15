using Microsoft.AspNetCore.Mvc;
using GettingStarted.Server.BUS;
using GettingStarted.Shared.Models;


namespace GettingStarted.Server.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : Controller
    {
    private readonly SinhVienService _sinhVienService;
    private readonly CaThiService _caThiService;
    private readonly ChiTietDeThiHoanViService _chiTietDeThiHoanViService;
    private readonly CauHoiService _cauHoiService;
    public ResultController(SinhVienService sinhVienService, ChiTietDeThiHoanViService chiTietDeThiHoanViService, CaThiService caThiService,
        CauHoiService cauHoiService)
    {
        _sinhVienService = sinhVienService;
        _caThiService = caThiService;
        _chiTietDeThiHoanViService = chiTietDeThiHoanViService;
        _cauHoiService = cauHoiService;
    }
    [HttpPost("GetThongTinSinhVien")]
    public ActionResult<SinhVien> GetThongTinSinhVien([FromQuery] long ma_sinh_vien)
    {
        return _sinhVienService.SelectOne(ma_sinh_vien);
    }
    [HttpPost("GetThongTinCaThi")]
    public ActionResult<CaThi> GetThongTinCaThi([FromQuery] int ma_ca_thi)
    {
        return _caThiService.SelectOne(ma_ca_thi);
    }
    [HttpPost("GetListDapAn")]
    public ActionResult<List<int>> GetListDapAn([FromBody] List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis)
    {
        List<int> listDapAn = new List<int>();
        foreach(var chiTietDeThiHoanVi in chiTietDeThiHoanVis)
        {
            int dapAn = _cauHoiService.SelectDapAn(chiTietDeThiHoanVi.MaCauHoi);
            listDapAn.Add(dapAn);
        }
        return listDapAn;
    }
}

