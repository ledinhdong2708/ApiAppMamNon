using System;
using System.Collections.Generic;

namespace API_WebApplication.Models;

public partial class HocPhiModel2
{
    public int Id { get; set; }

    public int Role { get; set; }

    public int StudentId { get; set; }

    public string? Content { get; set; }

    public int UserId { get; set; }

    public string? Months { get; set; }

    public string? Years { get; set; }

    public decimal Total { get; set; }

    public decimal PhiOff { get; set; }

    public decimal ConLai { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? Sobuoioff {  get; set; }
    public bool IsCompleted { get; set; }
    public int? AppID { get; set; }
    public bool Status { get; set; }

    public virtual ICollection<HocPhiChiTietModel2> HocPhiChiTietModels { get; set; } = new List<HocPhiChiTietModel2>();
}
