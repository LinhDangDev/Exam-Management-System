using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Server.DAL.UnitOfWork;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class SinhVienService
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly IChiTietCaThiRepository _chiTietCaThiRepository;
        public SinhVienService(ISinhVienRepository sinhVienRepository, IChiTietCaThiRepository chiTietCaThiRepository)
        {
            _sinhVienRepository = sinhVienRepository;
            _chiTietCaThiRepository = chiTietCaThiRepository;
        }
        public int GetMaDeHV_FromChiTietCaThi(long maSinhVien)
        {
            IDataReader dataReader = _chiTietCaThiRepository.GetBy_MaSinhVien(maSinhVien);
            if(dataReader.Read())
            {
                return dataReader.GetInt32(3);
            }
            dataReader.Close();
            return -1;
        }
    }
}
