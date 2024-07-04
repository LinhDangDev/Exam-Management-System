
using GettingStarted.Server.BUS;

namespace GettingStarted.Server.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // Them cac service vao
            services.AddScoped<AudioListenedService>();
            services.AddScoped<CaThiService>();
            services.AddScoped<CauHoiMaService>();
            services.AddScoped<CauHoiService>();
            services.AddScoped<CauTraLoiService>();
            services.AddScoped<ChiTietBaiThiService>();
            services.AddScoped<ChiTietCaThiService>();
            services.AddScoped<ChiTietCauHoiMaService>();
            services.AddScoped<ChiTietDeThiHoanViService>();
            services.AddScoped<ChiTietDeThiService>();
            services.AddScoped<ChiTietDotThiService>();
            services.AddScoped<DanhMucCaThiTrongNgayService>();
            services.AddScoped<DeThiHoanViService>();
            services.AddScoped<DeThiService>();
            services.AddScoped<DotThiService>();
            services.AddScoped<KhoaService>();
            services.AddScoped<LopAoService>();
            services.AddScoped<LopService>();
            services.AddScoped<MenuService>();
            services.AddScoped<MonHocService>();
            services.AddScoped<NhomCauHoiHoanViService>();
            services.AddScoped<NhomCauHoiService>();
            services.AddScoped<SinhVienLopAoService>();
            services.AddScoped<SinhVienService>();
            services.AddScoped<UserService>();
            services.AddScoped<CustomDeThiService>();
        }
    }
}
