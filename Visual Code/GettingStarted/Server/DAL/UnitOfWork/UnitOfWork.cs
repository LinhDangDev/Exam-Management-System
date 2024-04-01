using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;

namespace GettingStarted.Server.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private ISinhVienRepository _sinhvienRepo;
        public UnitOfWork(ApplicationDbContext context, ISinhVienRepository sinhVienRepository)
        {
            _context = context;
        }

        public ISinhVienRepository SinhViens
        {
            get
            {
                if(_sinhvienRepo == null)
                {
                    _sinhvienRepo = new SinhVienRepository(_context);
                }
                return _sinhvienRepo;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
