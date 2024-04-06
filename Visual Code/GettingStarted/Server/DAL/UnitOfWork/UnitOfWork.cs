using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using System.Security.Cryptography;

namespace GettingStarted.Server.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IAudioListenedRepository audioListenedRepository;
        private ICaThiRepository caThiRepository;
        private ICauHoiMaRepository cauHoiMaRepository;
        private ICauHoiRepository cauHoiRepository;
        private ICauTraLoiRepository cauTraLoiRepository;
        private IChiTietBaiThiRepository chiTietBaiThiRepository;
        private IChiTietCaThiRepository chiTietCaThiRepository;
        private IChiTietCauHoiMaRepository chiTietCauHoiMaRepository;
        private IChiTietDeThiHoanViRepository chiTietDeThiHoanViRepository;
        private IChiTietDeThiRepository chiTietDeThiRepository;
        private IChiTietDotThiResposity chiTietDotThiResposity;
        private IDanhMucCaThiTrongNgayRepository danhMucCaThiTrongNgayRepository;
        private IDeThiHoanViRepository deThiHoanViRepository;
        private IDeThiRepository deThiRepository;
        private IDotThiRepository dotThiRepository;
        private IKhoaRepository khoaRepository;
        private ILopAoRepository lopAoRepository;
        private ILopRepository lopRepository;
        private IMenuRepository menuRepository;
        private IMonHocRepository monHocRepository;
        private INhomCauHoiHoanViRepository nhomCauHoiHoanViRepository;
        private INhomCauHoiRepository nhomCauHoiRepository;
        private ISinhVienLopAoRepository sinhVienLopAoRepository;
        private ISinhVienRepository sinhVienRepository;
        private IUserRepository userRepository;
        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
        }


        public IAudioListenedRepository AudioListeneds
        {
            get
            {
                if(audioListenedRepository == null)
                {
                    audioListenedRepository = new AudioListenedRepository();
                }
                return audioListenedRepository;
            }
        }

        public ICaThiRepository CaThis
        {
            get
            {
                if (caThiRepository == null)
                {
                    caThiRepository = new CaThiRepository();
                }
                return caThiRepository;
            }
        }

        public ICauHoiMaRepository CauHoiMas
        {
            get
            {
                if (cauHoiMaRepository == null)
                {
                    cauHoiMaRepository = new CauHoiMaRepository();
                }
                return cauHoiMaRepository;
            }
        }

        public ICauHoiRepository CauHois
        {
            get
            {
                if (cauHoiRepository == null)
                {
                    cauHoiRepository = new CauHoiRepository();
                }
                return cauHoiRepository;
            }
        }

        public ICauTraLoiRepository CauTraLois
        {
            get
            {
                if (cauTraLoiRepository == null)
                {
                    cauTraLoiRepository = new CauTraLoiRepository();
                }
                return cauTraLoiRepository;
            }
        }

        public IChiTietBaiThiRepository ChiTietBaiThis
        {
            get
            {
                if (chiTietBaiThiRepository == null)
                {
                    chiTietBaiThiRepository = new ChiTietBaiThiRepository();
                }
                return chiTietBaiThiRepository;
            }
        }

        public IChiTietCaThiRepository ChiTietCaThis
        {
            get
            {
                if (chiTietCaThiRepository == null)
                {
                    chiTietCaThiRepository = new ChiTietCaThiRepository();
                }
                return chiTietCaThiRepository;
            }
        }

        public IChiTietCauHoiMaRepository ChiTietCauHoiMas
        {
            get
            {
                if (chiTietCauHoiMaRepository == null)
                {
                    chiTietCauHoiMaRepository = new ChiTietCauHoiMaRepository();
                }
                return chiTietCauHoiMaRepository;
            }
        }

        public IChiTietDeThiHoanViRepository ChiTietDeThiHoanVis
        {
            get
            {
                if (chiTietDeThiHoanViRepository == null)
                {
                    chiTietDeThiHoanViRepository = new ChiTietDeThiHoanViRepository();
                }
                return chiTietDeThiHoanViRepository;
            }
        }

        public IChiTietDeThiRepository ChiTietDeThis
        {
            get
            {
                if (chiTietDeThiRepository == null)
                {
                    chiTietDeThiRepository = new ChiTietDeThiRepository();
                }
                return chiTietDeThiRepository;
            }
        }

        public IChiTietDotThiResposity ChiTietDotThis
        {
            get
            {
                if (chiTietDotThiResposity == null)
                {
                    chiTietDotThiResposity = new ChiTietDotThiResposity();
                }
                return chiTietDotThiResposity;
            }
        }

        public IDanhMucCaThiTrongNgayRepository DanhMucCaThiTrongNgays
        {
            get
            {
                if (danhMucCaThiTrongNgayRepository == null)
                {
                    danhMucCaThiTrongNgayRepository = new DanhMucCaThiTrongNgayRepository();
                }
                return danhMucCaThiTrongNgayRepository;
            }
        }

        public IDeThiHoanViRepository DeThiHoanVis
        {
            get
            {
                if (deThiHoanViRepository == null)
                {
                    deThiHoanViRepository = new DeThiHoanViRepository();
                }
                return deThiHoanViRepository;
            }
        }

        public IDeThiRepository DeThis
        {
            get
            {
                if (deThiRepository == null)
                {
                    deThiRepository = new DeThiRepository();
                }
                return deThiRepository;
            }
        }

        public IDotThiRepository DotThis
        {
            get
            {
                if (dotThiRepository == null)
                {
                    dotThiRepository = new DotThiRepository();
                }
                return dotThiRepository;
            }
        }

        public IKhoaRepository Khoas
        {
            get
            {
                if (khoaRepository == null)
                {
                    khoaRepository = new KhoaRepository();
                }
                return khoaRepository;
            }
        }

        public ILopAoRepository LopAos
        {
            get
            {
                if (lopAoRepository == null)
                {
                    lopAoRepository = new LopAoRepository();
                }
                return lopAoRepository;
            }
        }

        public ILopRepository Lops
        {
            get
            {
                if (lopRepository == null)
                {
                    lopRepository = new LopRepository();
                }
                return lopRepository;
            }
        }

        public IMenuRepository Menus
        {
            get
            {
                if (menuRepository == null)
                {
                    menuRepository = new MenuRepository();
                }
                return menuRepository;
            }
        }

        public IMonHocRepository MonHocs
        {
            get
            {
                if (monHocRepository == null)
                {
                    monHocRepository = new MonHocRepository();
                }
                return monHocRepository;
            }
        }

        public INhomCauHoiHoanViRepository NhomCauHoiHoanVis
        {
            get
            {
                if (nhomCauHoiHoanViRepository == null)
                {
                    nhomCauHoiHoanViRepository = new NhomCauHoiHoanViRepository();
                }
                return nhomCauHoiHoanViRepository;
            }
        }

        public INhomCauHoiRepository NhomCauHois
        {
            get
            {
                if (nhomCauHoiRepository == null)
                {
                    nhomCauHoiRepository = new NhomCauHoiRepository();
                }
                return nhomCauHoiRepository;
            }
        }

        public ISinhVienLopAoRepository SinhVienLopAos
        {
            get
            {
                if (sinhVienLopAoRepository == null)
                {
                    sinhVienLopAoRepository = new SinhVienLopAoRepository();
                }
                return sinhVienLopAoRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository();
                }
                return userRepository;
            }
        }

        public ISinhVienRepository SinhViens
        {
            get
            {
                if (sinhVienRepository == null)
                {
                    sinhVienRepository = new SinhVienRepository();
                }
                return sinhVienRepository;
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
