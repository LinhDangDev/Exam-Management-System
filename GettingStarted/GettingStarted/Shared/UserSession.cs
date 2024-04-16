using GettingStarted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingStarted.Shared
{
    public class UserSession
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public int ExpireIn { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }
        public SinhVien NavigateSinhVien { get; set; }
    }
}
