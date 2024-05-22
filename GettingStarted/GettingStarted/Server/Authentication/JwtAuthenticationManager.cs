using GettingStarted.Server.BUS;
using GettingStarted.Shared;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GettingStarted.Server.Authentication
{
    public class JwtAuthenticationManager
    {
        public const string JWT_SECURITY_KEY = "yPkCqn4kSWLtaJwXvN4jGzQRyTZ3gdXkt7FeBJPLLD";
        public const int JWT_TOKEN_VALIDITY_MINS = 180;

        private SinhVienService _sinhVienService;

        public JwtAuthenticationManager(SinhVienService sinhVienService)
        {
            _sinhVienService = sinhVienService;
        }
        public UserSession? GenerateJwtToken(string username)
        {
            //username chính là ma_so_sinh_vien
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            /*Xác thực sinh viên có tồn tại trong database không ?*/
            SinhVien sinhVien = _sinhVienService.SelectBy_ma_so_sinh_vien(username);
            if (sinhVien == null || sinhVien.MaSoSinhVien == null)
            {
                return null;
            }
            /*Tạo JWT token*/
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                // claim lưu là mã sinh viên
                new Claim(ClaimTypes.Name, sinhVien.MaSinhVien.ToString())
            });
            var sigingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = sigingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            /*Trả dữ liệu về UserSession*/
            var userSession = new UserSession
            {
                Username = sinhVien.MaSoSinhVien,
                Token = token,
                ExpireIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                NavigateSinhVien = sinhVien
            };
            return userSession;
        }
    }
}
