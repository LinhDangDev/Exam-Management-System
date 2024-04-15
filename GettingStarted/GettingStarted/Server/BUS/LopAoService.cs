using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;
using System.Data.Common;

namespace GettingStarted.Server.BUS
{
    public class LopAoService
    {
        private readonly ILopAoRepository _lopAoRepository;
        public LopAoService(ILopAoRepository lopAoRepository)
        {
            _lopAoRepository = lopAoRepository;
        }
        private LopAo getProperty(IDataReader dataReader)
        {
            LopAo lopAo = new LopAo();
            lopAo.MaLopAo = dataReader.GetInt32(0);
            lopAo.TenLopAo = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
            lopAo.NgayBatDau = dataReader.IsDBNull(2) ? null : dataReader.GetDateTime(2);
            lopAo.MaMonHoc = dataReader.IsDBNull(3) ? null : dataReader.GetInt32(3);
            return lopAo;
        }
        public LopAo SelectOne(int ma_lop_ao)
        {
            LopAo lopAo = new LopAo();
            using(IDataReader dataReader = _lopAoRepository.SelectOne(ma_lop_ao))
            {
                if (dataReader.Read())
                {
                    lopAo = getProperty(dataReader);
                }
                dataReader.Dispose();
            }
            return lopAo;
        }
        public List<LopAo> SelectBy_ma_mon_hoc(int ma_mon_hoc)
        {
            List<LopAo> list = new List<LopAo>();
            using(IDataReader dataReader = _lopAoRepository.SelectBy_ma_mon_hoc(ma_mon_hoc))
            {
                while (dataReader.Read())
                {
                    LopAo lopAo = getProperty(dataReader);
                    list.Add(lopAo);
                }
                dataReader.Dispose();
            }
            return list;
        }
        public List<LopAo> SelectBy_ListChiTietDotThi(List<ChiTietDotThi> list)
        {
            List<LopAo> result = new List<LopAo>();
            foreach(var chiTietDotThi in list)
            {
                LopAo lopAo = this.SelectOne(chiTietDotThi.MaLopAo);
                // tránh bị trùng lặp
                if (!result.Contains(lopAo))
                {
                    result.Add(lopAo);
                }
            }
            return result;
        }
    }
}
