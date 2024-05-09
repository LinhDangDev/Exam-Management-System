using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class ChiTietBaiThiService
    {
        private readonly IChiTietBaiThiRepository _chiTietBaiThiRepository;
        public ChiTietBaiThiService(IChiTietBaiThiRepository chiTietBaiThiRepository)
        {
            _chiTietBaiThiRepository = chiTietBaiThiRepository;
        }
        private ChiTietBaiThi getProperty(IDataReader dataReader)
        {
            ChiTietBaiThi chiTietBaiThi = new ChiTietBaiThi();
            chiTietBaiThi.MaChiTietBaiThi = dataReader.GetInt64(0);
            chiTietBaiThi.MaChiTietCaThi = dataReader.GetInt32(1);
            chiTietBaiThi.MaDeHv = dataReader.GetInt64(2);
            chiTietBaiThi.MaNhom = dataReader.GetInt32(3);
            chiTietBaiThi.MaCauHoi = dataReader.GetInt32(4);
            chiTietBaiThi.CauTraLoi = dataReader.IsDBNull(5) ? null : dataReader.GetInt32(5);
            chiTietBaiThi.NgayTao = dataReader.GetDateTime(6);
            chiTietBaiThi.NgayCapNhat = dataReader.IsDBNull(7) ? null : dataReader.GetDateTime(7);
            chiTietBaiThi.KetQua = dataReader.IsDBNull(8) ? null : dataReader.GetBoolean(8);
            chiTietBaiThi.ThuTu = dataReader.GetInt32(9);
            return chiTietBaiThi;
        }
        public void Insert(int ma_chi_tiet_ca_thi, long MaDeHV, int MaNhom, int MaCauHoi, DateTime NgayTao, int ThuTu)
        {
            try
            {
                _chiTietBaiThiRepository.Insert(ma_chi_tiet_ca_thi, MaDeHV, MaNhom, MaCauHoi, NgayTao, ThuTu);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //if(!_chiTietBaiThiRepository.Insert(ma_chi_tiet_ca_thi, MaDeHV, MaNhom, MaCauHoi, NgayTao, ThuTu))
            //{
            //    throw new Exception("Can not insert ChiTietBaiThi");
            //}
        }
        public void Update(long MaChiTietBaiThi, int CauTraLoi, DateTime NgayCapNhat, bool KetQua)
        {
            if(!_chiTietBaiThiRepository.Update(MaChiTietBaiThi, CauTraLoi, NgayCapNhat, KetQua))
            {
                throw new Exception("Can not update ChiTietBaiThi");
            }
        }
        public List<ChiTietBaiThi> SelectBy_ma_chi_tiet_ca_thi(int ma_chi_tiet_ca_thi)
        {
            List<ChiTietBaiThi> result = new List<ChiTietBaiThi>();
            using(IDataReader dataReader = _chiTietBaiThiRepository.SelectBy_ma_chi_tiet_ca_thi(ma_chi_tiet_ca_thi))
            {
                while(dataReader.Read())
                {
                    ChiTietBaiThi chiTietBaiThi = getProperty(dataReader);
                    result.Add(chiTietBaiThi);
                }
                dataReader.Dispose();
            }
            return result;
            
        }
        public void insertChiTietBaiThis_SelectByChiTietDeThiHV(List<TblChiTietDeThiHoanVi> chiTietDeThiHoanVis, int ma_chi_tiet_ca_thi)
        {
            foreach(var item in chiTietDeThiHoanVis)
            {
                this.Insert(ma_chi_tiet_ca_thi, item.MaDeHv, item.MaNhom, item.MaCauHoi, DateTime.Now, item.ThuTu);
            }
        }
        public void updateChiTietBaiThis(List<ChiTietBaiThi> chiTietBaiThis)
        {
            foreach(var item in chiTietBaiThis)
            {
                if(item.CauTraLoi != null && item.KetQua != null)
                {
                    this.Update(item.MaChiTietBaiThi, (int)item.CauTraLoi, DateTime.Now, (bool)item.KetQua);
                }
            }
        }
    }
}
