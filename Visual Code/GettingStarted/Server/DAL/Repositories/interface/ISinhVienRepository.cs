using GettingStarted.Shared.Models;

namespace GettingStarted.Server.DAL.Repositories
{
    public interface ISinhVienRepository
    {
        public void Insert(SinhVien sinhVien);
        public void Update(SinhVien sinhVien);
        public void Delete(SinhVien sinhVien);

    }
}
