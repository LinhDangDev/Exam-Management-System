using GettingStarted.Server.BUS;
using GettingStarted.Server.DAL.Repositories;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);


/////Build AddScope

builder.Services.AddScoped<IAudioListenedRepository, AudioListenedRepository>();
builder.Services.AddScoped<ICaThiRepository, CaThiRepository>();
builder.Services.AddScoped<ICauHoiMaRepository, CauHoiMaRepository>();
builder.Services.AddScoped<ICauHoiRepository, CauHoiRepository>();
builder.Services.AddScoped<ICauTraLoiRepository, CauTraLoiRepository>();
builder.Services.AddScoped<IChiTietBaiThiRepository,  ChiTietBaiThiRepository>();
builder.Services.AddScoped<IChiTietCaThiRepository, ChiTietCaThiRepository>();
builder.Services.AddScoped<IChiTietCauHoiMaRepository, ChiTietCauHoiMaRepository>();
builder.Services.AddScoped<IChiTietDeThiHoanViRepository, ChiTietDeThiHoanViRepository>();
builder.Services.AddScoped<IChiTietDeThiRepository, ChiTietDeThiRepository>();
builder.Services.AddScoped<IChiTietDotThiResposity, ChiTietDotThiResposity>();
builder.Services.AddScoped<IDanhMucCaThiTrongNgayRepository, DanhMucCaThiTrongNgayRepository>();
builder.Services.AddScoped<IDeThiHoanViRepository, DeThiHoanViRepository>();
builder.Services.AddScoped<IDeThiRepository, DeThiRepository>();
builder.Services.AddScoped<IDotThiRepository, DotThiRepository>();
builder.Services.AddScoped<IKhoaRepository, KhoaRepository>();
builder.Services.AddScoped<ILopAoRepository, LopAoRepository>();
builder.Services.AddScoped<ILopRepository, LopRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMonHocRepository, MonHocRepository>();
builder.Services.AddScoped<INhomCauHoiHoanViRepository, NhomCauHoiHoanViRepository>();
builder.Services.AddScoped<INhomCauHoiRepository, NhomCauHoiRepository>();
builder.Services.AddScoped<ISinhVienLopAoRepository, SinhVienLopAoRepository>();
builder.Services.AddScoped<ISinhVienRepository, SinhVienRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

/////////////////////////////


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
