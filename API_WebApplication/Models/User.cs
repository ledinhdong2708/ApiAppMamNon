using System;
using System.Collections.Generic;

namespace API_WebApplication.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? PasswordSalt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? Avatar { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public string? Address2 { get; set; }

    public int Role { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? TS { get; set; }

    public bool Active { get; set; }

    public string? Position { get; set; }

    public string? Username { get; set; }

    public int? StudentId { get; set; }

    public int? AppID { get; set; }

    public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<TableImage> TableImages { get; set; } = new List<TableImage>();

    public virtual ICollection<TableLike> TableLikes { get; set; } = new List<TableLike>();
}
