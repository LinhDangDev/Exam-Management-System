using System;
using System.Collections.Generic;

namespace GettingStarted.Shared.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string LoginName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsLockedOut { get; set; }

    public DateTime? LastActivityDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? LastPasswordChangedDate { get; set; }

    public DateTime? LastLockoutDate { get; set; }

    public int? FailedPwdAttemptCount { get; set; }

    public DateTime? FailedPwdAttemptWindowStart { get; set; }

    public int? FailedPwdAnswerCount { get; set; }

    public DateTime? FailedPwdAnswerWindowStart { get; set; }

    public string? PasswordSalt { get; set; }

    public string? Comment { get; set; }

    public bool IsBuildInUser { get; set; }
}
