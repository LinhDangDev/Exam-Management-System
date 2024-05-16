using GettingStarted.Server.Authentication;
using GettingStarted.Server.BUS;
using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Them cac dich vu service
ConfigureServices(builder.Services);


/*Database Context Depedency Injection*/

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var connectionString = $"Data Source={dbHost},Initial Catalog={dbName}";
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));
/*===========================================*/




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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    services.AddRazorPages();
    services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtAuthenticationManager.JWT_SECURITY_KEY)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

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
    services.AddScoped<LopService>();
    services.AddScoped<MenuService>();
    services.AddScoped<MonHocService>();
    services.AddScoped<NhomCauHoiHoanViService>();
    services.AddScoped<NhomCauHoiService>();
    services.AddScoped<SinhVienLopAoService>();
    services.AddScoped<SinhVienService>();
    services.AddScoped<UserService>();
}