using System;
using System.Collections.Generic;

namespace API_WebApplication.Models;

public partial class ChiTietHocPhiTheoMonth
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public string? Months { get; set; }

    public string? Years { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public decimal Total { get; set; }

    public int HocPhiChiTietId { get; set; }

    public int UserId { get; set; }

    public int StudentId { get; set; }

    public int? AppID { get; set; }

    public virtual ChiTietHocPhi HocPhiChiTiet { get; set; }
}
