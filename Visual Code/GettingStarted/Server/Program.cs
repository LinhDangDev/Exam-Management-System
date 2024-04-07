using GettingStarted.Server.BUS;
using GettingStarted.Server.DAL.Repositories;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

//Them cac dich vu service
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    services.AddRazorPages();

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
    services.AddScoped<LopAoService>();
    services.AddScoped<MenuService>();
    services.AddScoped<MonHocService>();
    services.AddScoped<NhomCauHoiHoanViService>();
    services.AddScoped<CauHoiService>();
    services.AddScoped<SinhVienLopAoService>();
    services.AddScoped<SinhVienService>();
    services.AddScoped<UserService>();
}