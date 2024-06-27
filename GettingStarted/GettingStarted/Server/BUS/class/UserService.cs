using GettingStarted.Server.DAL.Repositories;
using GettingStarted.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        private User getProperty(IDataReader dataReader)
        {
            User user = new User();
            user.UserId = dataReader.GetGuid(0);
            user.LoginName = dataReader.GetString(1);
            user.Email = dataReader.GetString(2);
            user.Name = dataReader.GetString(3);
            user.Password = dataReader.GetString(4);
            user.DateCreated = dataReader.GetDateTime(5);
            user.IsDeleted = dataReader.GetBoolean(6);
            user.IsLockedOut = dataReader.GetBoolean(7);
            user.LastActivityDate = dataReader.IsDBNull(8) ? null : dataReader.GetDateTime(8);
            user.LastLoginDate = dataReader.IsDBNull(9) ? null : dataReader.GetDateTime(9);
            user.LastPasswordChangedDate = dataReader.IsDBNull(10) ? null : dataReader.GetDateTime(10);
            user.LastLockoutDate = dataReader.IsDBNull(11) ? null : dataReader.GetDateTime(11);
            user.FailedPwdAttemptCount = dataReader.IsDBNull(12) ? null : dataReader.GetInt32(12);
            user.FailedPwdAttemptWindowStart = dataReader.IsDBNull(13) ? null : dataReader.GetDateTime(13);
            user.FailedPwdAnswerCount = dataReader.IsDBNull(14) ? null : dataReader.GetInt32(14);
            user.FailedPwdAnswerWindowStart = dataReader.IsDBNull(15) ? null : dataReader.GetDateTime(15);
            user.PasswordSalt = dataReader.IsDBNull(16) ? null : dataReader.GetString(16);
            user.Comment = dataReader.GetString(17);
            user.IsBuildInUser = dataReader.GetBoolean(18);
            return user;
        }
        public User SelectOne(Guid userId)
        {
            User user = new User();
            using (IDataReader dataReader = _userRepository.SelectOne(userId))
            {
                if (dataReader.Read())
                {
                    user = getProperty(dataReader);
                }
            }
            return user;
        }
        public User SelectByLoginName(string loginName)
        {
            User user = new User();
            using (IDataReader dataReader = _userRepository.SelectByLoginName(loginName))
            {
                if (dataReader.Read())
                {
                    user = getProperty(dataReader);
                }
            }
            return user;
        }
    }
}
