using GettingStarted.Server.DAL.Repositories;

namespace GettingStarted.Server.BUS
{
    public class ChiTietCaThiService
    {
        private readonly IChiTietCaThiRepository _chiTietCaThiRepository;
        public ChiTietCaThiService(IChiTietCaThiRepository chiTietCaThiRepository)
        {
            _chiTietCaThiRepository = chiTietCaThiRepository;
        }
    }
}
