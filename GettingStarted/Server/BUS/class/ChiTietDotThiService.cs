using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Collections.Generic;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class ChiTietDotThiService
    {
        private readonly IChiTietDotThiResposity _chiTietDotThiResposity;
        public ChiTietDotThiService(IChiTietDotThiResposity chiTietDotThiRepository)
        {
            _chiTietDotThiResposity = chiTietDotThiRepository;
        }
        private ChiTietDotThi getProperty(IDataReader dataReader)
        {
            ChiTietDotThi chiTietDotThi = new ChiTietDotThi();
            chiTietDotThi.MaChiTietDotThi = dataReader.GetInt32(0);
            chiTietDotThi.TenChiTietDotThi = dataReader.GetString(1);
            chiTietDotThi.MaLopAo = dataReader.GetInt32(2);
            chiTietDotThi.MaDotThi = dataReader.GetInt32(3);
            chiTietDotThi.LanThi = dataReader.GetString(4);
            return chiTietDotThi;
        }
        public List<ChiTietDotThi> SelectBy_MaDotThi(int ma_dot_thi)
        {
            List<ChiTietDotThi> list = new List<ChiTietDotThi>();
            using(IDataReader dataReader = _chiTietDotThiResposity.SelectBy_MaDotThi(ma_dot_thi))
            {
                while(dataReader.Read())
                {
                    ChiTietDotThi chiTietDotThi = getProperty(dataReader);
                    list.Add(chiTietDotThi);
                }
            }
            return list;
        }
        public ChiTietDotThi SelectBy_MaDotThi_MaLopAo(int ma_dot_thi, int ma_lop_ao)
        {
            ChiTietDotThi chiTietDotThi = new ChiTietDotThi();
            using (IDataReader dataReader = _chiTietDotThiResposity.SelectBy_MaDotThi_MaLopAo(ma_dot_thi, ma_lop_ao))
            {

                if (dataReader.Read())
                {
                    chiTietDotThi = getProperty(dataReader);
                }
            }
            return chiTietDotThi;
        }
        public ChiTietDotThi SelectOne(int ma_chi_tiet_dot_thi)
        {
            ChiTietDotThi chiTietDotThi = new ChiTietDotThi();
            using (IDataReader dataReader = _chiTietDotThiResposity.SelectOne(ma_chi_tiet_dot_thi))
            {

                if (dataReader.Read())
                {
                    chiTietDotThi = getProperty(dataReader);
                }
            }
            return chiTietDotThi;
        }
    }
}
