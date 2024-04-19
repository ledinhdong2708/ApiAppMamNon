using System;
using System.Collections.Generic;

namespace API_WebApplication.Models;

public partial class GiaTiensModel
{
    public int Id { get; set; }
    public string name { get; set; }

    public decimal gia { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
