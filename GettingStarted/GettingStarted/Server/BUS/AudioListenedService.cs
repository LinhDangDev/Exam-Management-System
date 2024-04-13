using GettingStarted.Server.DAL.Repositories;

namespace GettingStarted.Server.BUS
{
    public class AudioListenedService
    {
        IAudioListenedRepository audioListenedRepository = new AudioListenedRepository(); 
    }
}
