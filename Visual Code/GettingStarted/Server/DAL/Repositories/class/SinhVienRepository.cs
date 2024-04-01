using GettingStarted.Server.BUS;
using GettingStarted.Shared.Models;

namespace GettingStarted.Server.DAL.Repositories
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private ApplicationDbContext _context;
        DatabaseService _databaseService;

        public SinhVienRepository(ApplicationDbContext context, DatabaseService databaseService)
        {
            _context = context;
            _databaseService = databaseService;
        }
        public void Insert(SinhVien sinhVien)
        {
            _databaseService.CreateSqlCommandProcedure("sinh_vien_Insert", "Dung ");
        }
        public void Delete(SinhVien sinhVien)
        {
            throw new NotImplementedException();
        }

        public void Update(SinhVien sinhVien)
        {
            throw new NotImplementedException();
        }
    }
}
