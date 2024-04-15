using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;
using System.Data.Common;

namespace GettingStarted.Server.BUS
{
    public class MonHocService
    {
        private readonly IMonHocRepository _monHocRepository;
        public MonHocService(IMonHocRepository monHocRepository)
        {
            _monHocRepository = monHocRepository;
        }
        public MonHoc SelectOne(int ma_mon_hoc)
        {
            MonHoc monHoc = new MonHoc();
            using (IDataReader dataReader = _monHocRepository.SelectOne(ma_mon_hoc))
            {
                if (dataReader.Read())
                {
                    monHoc.MaMonHoc = dataReader.GetInt32(0);
                    monHoc.MaSoMonHoc = dataReader.IsDBNull(1) ? null : dataReader.GetString(1);
                    monHoc.TenMonHoc = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);
                }
                dataReader.Dispose();
            }
            return monHoc;
        }
        public List<MonHoc> SelectBy_ListLopAo(List<LopAo> list)
        {
            List<MonHoc> result = new List<MonHoc>();
            foreach(var lopAo in list)
            {
                MonHoc monHoc = this.SelectOne((int)lopAo.MaMonHoc);
                if (!result.Contains(monHoc))
                {
                    result.Add(monHoc);
                }
            }
            return result;
        }
    }
}
