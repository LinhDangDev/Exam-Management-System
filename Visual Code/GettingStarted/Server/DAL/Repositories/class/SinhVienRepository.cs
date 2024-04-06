using GettingStarted.Server.BUS;
using GettingStarted.Server.DAL.DataReader;
using GettingStarted.Shared.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GettingStarted.Server.DAL.Repositories
{
    public class SinhVienRepository : ISinhVienRepository
    {
        public int Insert(string ho_va_ten_lot, string ten_sinh_vien, int gioi_tinh, DateTime ngay_sinh, int ma_lop,
            string dia_chi, string email, string dien_thoai, string ma_so_sinh_vien, Guid student_id)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_Insert");
            sql.SqlParams("@ho_va_ten_lot", SqlDbType.NVarChar, ho_va_ten_lot);
            sql.SqlParams("@ten_sinh_vien", SqlDbType.NVarChar, ten_sinh_vien);
            sql.SqlParams("@gioi_tinh", SqlDbType.SmallInt, gioi_tinh);
            sql.SqlParams("@ngay_sinh", SqlDbType.DateTime, ngay_sinh);
            sql.SqlParams("@ma_lop", SqlDbType.Int, ma_lop);
            sql.SqlParams("@dia_chi", SqlDbType.Text, dia_chi);
            sql.SqlParams("@email", SqlDbType.NVarChar, email);
            sql.SqlParams("@dien_thoai", SqlDbType.NVarChar, dien_thoai);
            sql.SqlParams("@ma_so_sinh_vien", SqlDbType.NVarChar, ma_so_sinh_vien);
            sql.SqlParams("@student_id", SqlDbType.UniqueIdentifier, student_id);
            return sql.ExcuteNonQuery();
        }
        public bool Update(long ma_sinh_vien, string ho_va_ten_lot, string ten_sinh_vien, int gioi_tinh, 
            DateTime ngay_sinh, int ma_lop, string dia_chi, string email, string dien_thoai, string ma_so_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_Update");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            sql.SqlParams("@ho_va_ten_lot", SqlDbType.NVarChar, ho_va_ten_lot);
            sql.SqlParams("@ten_sinh_vien", SqlDbType.NVarChar, ten_sinh_vien);
            sql.SqlParams("@gioi_tinh", SqlDbType.SmallInt, gioi_tinh);
            sql.SqlParams("@ngay_sinh", SqlDbType.DateTime, ngay_sinh);
            sql.SqlParams("@ma_lop", SqlDbType.Int, ma_lop);
            sql.SqlParams("@dia_chi", SqlDbType.Text, dia_chi);
            sql.SqlParams("@email", SqlDbType.NVarChar, email);
            sql.SqlParams("@dien_thoai", SqlDbType.NVarChar, dien_thoai);
            sql.SqlParams("@ma_so_sinh_vien", SqlDbType.NVarChar, ma_so_sinh_vien);
            int result = sql.ExcuteNonQuery();
            return result == 1;
        }
        public bool Remove(long ma_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_Remove");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            int result = sql.ExcuteNonQuery();
            return result == 1;
        }
        public IDataReader GetSinhVien(long ma_sinh_vien)
        {
            DatabaseReader sql = new DatabaseReader("sinh_vien_SelectOne");
            sql.SqlParams("@ma_sinh_vien", SqlDbType.BigInt, ma_sinh_vien);
            return sql.ExcuteReader();
        }

        
    }
}
