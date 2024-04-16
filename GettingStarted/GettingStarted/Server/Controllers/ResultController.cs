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
    private readonly CauTraLoiService _cauTraLoiService;
    private readonly ChiTietCaThiService _chiTietCaThiService;
    public ResultController(SinhVienService sinhVienService, ChiTietDeThiHoanViService chiTietDeThiHoanViService, CaThiService caThiService,
        CauTraLoiService cauTraLoiService, ChiTietCaThiService chiTietCaThiService)
    {
        _sinhVienService = sinhVienService;
        _caThiService = caThiService;
        _chiTietDeThiHoanViService = chiTietDeThiHoanViService;
        _cauTraLoiService = cauTraLoiService;
        _chiTietCaThiService = chiTietCaThiService;
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
    [HttpPost("GetChiTietCaThiSelectBy_SinhVien")]
    // lấy chi tiết các thông tin của 1 sinh viên thi vào 1 ca giờ cụ thể (đề thi hoán vị)
    public ActionResult<ChiTietCaThi> GetChiTietCaThiSelectBy_SinhVien([FromQuery] int ma_ca_thi, [FromQuery] long ma_sinh_vien)
    {
        return _chiTietCaThiService.SelectBy_MaCaThi_MaSinhVien(ma_ca_thi, ma_sinh_vien);
    }
    [HttpPost("GetListDapAn")]
    public ActionResult<List<int>> GetListDapAn([FromBody] List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis)
    {
        List<int> listDapAn = new List<int>();
        foreach(var chiTietDeThiHoanVi in chiTietDeThiHoanVis)
        {
            List<TblCauTraLoi> cauTraLois = _cauTraLoiService.SelectBy_MaCauHoi(chiTietDeThiHoanVi.MaCauHoi);
            TblCauTraLoi? cauTraLoi = cauTraLois.FirstOrDefault(p => p.LaDapAn);
            if (cauTraLoi != null)
                listDapAn.Add(cauTraLoi.ThuTu);
            else
                listDapAn.Add(-1); // phòng trường hợp nhập tay thiếu đáp án true :)))
        }
        return listDapAn;
    }
    [HttpPost("UpdateKetThuc")]
    public ActionResult UpdateKetThuc([FromBody] ChiTietCaThi chiTietCaThi)
    {
        _chiTietCaThiService.UpdateKetThuc(chiTietCaThi);
        return Ok();
    }
}

