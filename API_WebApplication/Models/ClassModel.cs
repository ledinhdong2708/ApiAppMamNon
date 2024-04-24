using System;
using System.Collections.Generic;

namespace API_WebApplication.Models;

public partial class ClassModel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int Role { get; set; }

    public string? NameClass { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool IsCompleted { get; set; }

    public int? AppID { get; set; }
}
