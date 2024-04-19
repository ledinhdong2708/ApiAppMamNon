using System;
using System.Collections.Generic;

namespace API_WebApplication.Models;

public partial class RefreshToken
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? TokenHash { get; set; }

    public string? TokenSalt { get; set; }

    public DateTime TS { get; set; }

    public DateTime ExpiryDate { get; set; }

    public virtual User User { get; set; }
}
