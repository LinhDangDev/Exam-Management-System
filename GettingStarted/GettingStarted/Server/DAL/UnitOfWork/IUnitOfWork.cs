using GettingStarted.Server.DAL.Repositories;

namespace GettingStarted.Server.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAudioListenedRepository AudioListeneds{ get; }
        ICaThiRepository CaThis { get; }
        ICauHoiMaRepository CauHoiMas { get; }
        ICauHoiRepository CauHois { get; }
        ICauTraLoiRepository CauTraLois { get; }
        IChiTietBaiThiRepository ChiTietBaiThis { get; }
        IChiTietCaThiRepository ChiTietCaThis { get; }
        IChiTietCauHoiMaRepository ChiTietCauHoiMas { get; }
        IChiTietDeThiHoanViRepository ChiTietDeThiHoanVis { get; }
        IChiTietDeThiRepository ChiTietDeThis { get; }
        IChiTietDotThiResposity ChiTietDotThis { get; }
        IDanhMucCaThiTrongNgayRepository DanhMucCaThiTrongNgays { get; }
        IDeThiHoanViRepository DeThiHoanVis { get; }
        IDeThiRepository DeThis { get; }
        IDotThiRepository DotThis { get; }
        IKhoaRepository Khoas { get; }
        ILopAoRepository LopAos { get; }
        ILopRepository Lops { get; }
        IMenuRepository Menus { get; }
        IMonHocRepository MonHocs { get; }
        INhomCauHoiHoanViRepository NhomCauHoiHoanVis { get; }
        INhomCauHoiRepository NhomCauHois { get; }
        ISinhVienLopAoRepository SinhVienLopAos { get; }
        ISinhVienRepository SinhViens { get; }
        IUserRepository Users { get; }

        Task<int> SaveChangesAsync();
    }
}
