using GettingStarted.Server.DAL.Repositories;

namespace GettingStarted.Server.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        ISinhVienRepository SinhViens { get; }
        

        Task<int> SaveChangesAsync();
    }
}
