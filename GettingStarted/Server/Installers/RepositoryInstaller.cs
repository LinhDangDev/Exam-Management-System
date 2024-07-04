
using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;

namespace GettingStarted.Server.Installers
{
    public class RepositoryInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // Them cac repository vao
            services.AddScoped<IAudioListenedRepository, AudioListenedRepository>();
            services.AddScoped<ICaThiRepository, CaThiRepository>();
            services.AddScoped<ICauHoiMaRepository, CauHoiMaRepository>();
            services.AddScoped<ICauHoiRepository, CauHoiRepository>();
            services.AddScoped<ICauTraLoiRepository, CauTraLoiRepository>();
            services.AddScoped<IChiTietBaiThiRepository, ChiTietBaiThiRepository>();
            services.AddScoped<IChiTietCaThiRepository, ChiTietCaThiRepository>();
            services.AddScoped<IChiTietCauHoiMaRepository, ChiTietCauHoiMaRepository>();
            services.AddScoped<IChiTietDeThiHoanViRepository, ChiTietDeThiHoanViRepository>();
            services.AddScoped<IChiTietDeThiRepository, ChiTietDeThiRepository>();
            services.AddScoped<IChiTietDotThiResposity, ChiTietDotThiResposity>();
            services.AddScoped<IDanhMucCaThiTrongNgayRepository, DanhMucCaThiTrongNgayRepository>();
            services.AddScoped<IDeThiHoanViRepository, DeThiHoanViRepository>();
            services.AddScoped<IDeThiRepository, DeThiRepository>();
            services.AddScoped<IDotThiRepository, DotThiRepository>();
            services.AddScoped<IKhoaRepository, KhoaRepository>();
            services.AddScoped<ILopAoRepository, LopAoRepository>();
            services.AddScoped<ILopRepository, LopRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMonHocRepository, MonHocRepository>();
            services.AddScoped<INhomCauHoiHoanViRepository, NhomCauHoiHoanViRepository>();
            services.AddScoped<INhomCauHoiRepository, NhomCauHoiRepository>();
            services.AddScoped<ISinhVienLopAoRepository, SinhVienLopAoRepository>();
            services.AddScoped<ISinhVienRepository, SinhVienRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<CustomDeThi>();
        }
    }
}
