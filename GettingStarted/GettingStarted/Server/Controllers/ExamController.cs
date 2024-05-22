using GettingStarted.Server.BUS;
using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GettingStarted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : Controller
    {
        private readonly NhomCauHoiService _nhomCauHoiService;
        private readonly ChiTietDeThiHoanViService _chiTietDeThiHoanViService;
        private readonly CauTraLoiService _cauTraLoiService;
        private readonly CauHoiService _cauHoiService;
        private readonly NhomCauHoiHoanViService _nhomCauHoiHoanViService;
        private readonly ChiTietBaiThiService _chiTietBaiThiService;
        private readonly AudioListenedService _audioListenedService;
        public ExamController(NhomCauHoiService nhomCauHoiService, ChiTietDeThiHoanViService chiTietDeThiHoanViService, CauTraLoiService cauTraLoiService,
            CauHoiService cauHoiService, NhomCauHoiHoanViService nhomCauHoiHoanViService, ChiTietBaiThiService chiTietBaiThiService, AudioListenedService audioListenedService)
        {
            _nhomCauHoiService = nhomCauHoiService;
            _chiTietDeThiHoanViService = chiTietDeThiHoanViService;
            _cauTraLoiService = cauTraLoiService;
            _cauHoiService = cauHoiService;
            _nhomCauHoiHoanViService = nhomCauHoiHoanViService;
            _chiTietBaiThiService = chiTietBaiThiService;
            _audioListenedService = audioListenedService;
        }
        [HttpPost("GetThongTinChiTietDeThiHV")]
        public ActionResult<List<TblChiTietDeThiHoanVi>> GetThongTinChiTietDeThiHV([FromQuery]long ma_de_thi_hoan_vi)
        {
            List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis = new List<TblChiTietDeThiHoanVi>();

            // bắt đầu từ NhomCauHoiHoanVi để lấy thứ tự nhóm câu hỏi
            List<TblNhomCauHoiHoanVi> nhomCauHoiHoanVis = _nhomCauHoiHoanViService.SelectBy_MaDeHV(ma_de_thi_hoan_vi);

            // lấy từng mã câu hỏi theo thứ tự của nhóm câu hỏi
            foreach(var item in nhomCauHoiHoanVis)
            {
                List<TblChiTietDeThiHoanVi> tempChiTietDeThiHoanVis = _chiTietDeThiHoanViService.SelectBy_MaDeHV_MaNhom(ma_de_thi_hoan_vi, item.MaNhom);
                chiTietDeThiHoanVis.AddRange(tempChiTietDeThiHoanVis);
            }

            //thêm các nhóm câu hỏi hoán vị cho từng đối tượng -> lấy cho đủ thông tin, vì nó không được null
            foreach (var item in chiTietDeThiHoanVis)
            {
                item.Ma = getThongTinNhomCauHoiHoanVi(item.MaDeHv, item.MaNhom);
            }
            return chiTietDeThiHoanVis;
        }
        private TblNhomCauHoiHoanVi getThongTinNhomCauHoiHoanVi(long ma_de_hoan_vi, int ma_nhom)
        {
            TblNhomCauHoiHoanVi nhomCauHoiHoanVi = _nhomCauHoiHoanViService.SelectOne(ma_de_hoan_vi, ma_nhom);
            return nhomCauHoiHoanVi;
        }
        [HttpPost("GetNoiDungCauHoiNhom")]
        public ActionResult<List<TblNhomCauHoi>> GetNoiDungCauHoiNhom([FromBody] List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis)
        {
            List<TblNhomCauHoi> result = new List<TblNhomCauHoi>();
            foreach (var chiTietDeThiHV in chiTietDeThiHoanVis)
            {
                TblNhomCauHoi nhomCauHoi = _nhomCauHoiService.SelectOne(chiTietDeThiHV.MaNhom);
                if (nhomCauHoi != null && !result.Contains(nhomCauHoi))
                    result.Add(nhomCauHoi);
            }
            return result;
        }

        [HttpPost("GetNoiDungCauHoi")]
        public ActionResult<List<TblCauHoi>> GetNoiDungCauHoi([FromBody] List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis)
        {
            List<TblCauHoi> cauHois = new List<TblCauHoi>();
            foreach (var chiTietDeThiHV in chiTietDeThiHoanVis)
            {
                TblCauHoi cauHoi = _cauHoiService.SelectOne(chiTietDeThiHV.MaCauHoi);
                cauHois.Add(cauHoi);
            }
            // liên kết với câu trả lời
            foreach(var item in cauHois)
            {
                item.TblCauTraLois = getNoiDungCauTraLoi(item.MaCauHoi);
            }
            return cauHois;
        }
        private List<TblCauTraLoi> getNoiDungCauTraLoi(int ma_cau_hoi)
        {
            return _cauTraLoiService.SelectBy_MaCauHoi(ma_cau_hoi);
        }
        [HttpPost("InsertChiTietBaiThi")]
        public ActionResult<List<ChiTietBaiThi>> InsertChiTietBaiThi([FromQuery] int ma_chi_tiet_ca_thi ,[FromBody] List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis)
        {
            _chiTietBaiThiService.insertChiTietBaiThis_SelectByChiTietDeThiHV(chiTietDeThiHoanVis, ma_chi_tiet_ca_thi);
            List<ChiTietBaiThi> chiTietBaiThis = _chiTietBaiThiService.SelectBy_ma_chi_tiet_ca_thi(ma_chi_tiet_ca_thi);
            return chiTietBaiThis.Where(p => p.MaDeHv == chiTietDeThiHoanVis[0].MaDeHv).ToList();
        }
        [HttpPost("UpdateChiTietBaiThi")]
        public ActionResult UpdateChiTietBaiThi([FromBody] List<ChiTietBaiThi> chiTietBaiThis)
        {
            _chiTietBaiThiService.updateChiTietBaiThis(chiTietBaiThis);
            return Ok();
        }
        [HttpPost("AudioListendCount")]
        public ActionResult<int> AudioListendCount([FromQuery] int ma_chi_tiet_ca_thi, [FromQuery] string filename)
        {
            return _audioListenedService.SelectOne(ma_chi_tiet_ca_thi, filename);
        }
    }
}
