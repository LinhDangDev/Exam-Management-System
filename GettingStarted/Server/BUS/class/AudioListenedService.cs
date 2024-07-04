using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class AudioListenedService
    {
        private readonly IAudioListenedRepository _audioListenedRepository;
        public AudioListenedService(IAudioListenedRepository audioListenedRepository)
        {
            _audioListenedRepository = audioListenedRepository;
        }
        private TblAudioListened getProperty(IDataReader dataReader)
        {
            TblAudioListened audioListened = new TblAudioListened();
            audioListened.ListenId = dataReader.GetInt64(0);
            audioListened.MaChiTietCaThi = dataReader.GetInt32(1);
            audioListened.FileName = dataReader.GetString(2);
            audioListened.ListenedCount = dataReader.GetInt32(3);
            return audioListened;
        }
        public int SelectOne(int ma_chi_tiet_ca_thi, string filename)
        {
            int listenedCount = 0;
            using (IDataReader dataReader = _audioListenedRepository.SelectOne(ma_chi_tiet_ca_thi, filename))
            {
                if (dataReader.Read())
                {
                    listenedCount = dataReader.GetInt32(0);
                }
            }
            return listenedCount;
        }
    }
}
