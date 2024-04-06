using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Server.DAL.UnitOfWork;

namespace GettingStarted.Server.BUS
{
    public class CauHoiService
    {
       ICauHoiMaRepository cauHoiMaRepository = new CauHoiMaRepository();
    }
}
