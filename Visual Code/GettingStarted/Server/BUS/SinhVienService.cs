using GettingStarted.Server.DAL.UnitOfWork;
using GettingStarted.Shared.Models;

namespace GettingStarted.Server.BUS
{
    public class SinhVienService
    {
        private readonly UnitOfWork _unitOfWork;
        public SinhVienService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void InsertSV(SinhVien sinhVien)
        {
            _unitOfWork.SinhViens.Insert(sinhVien);
        }
    }
}
