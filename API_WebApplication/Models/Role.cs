using System;
using System.Collections.Generic;

namespace API_WebApplication.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool Active { get; set; }

    public int? AppID { get; set; }
}
