using System;
using System.Collections.Generic;

namespace API_WebApplication.Models;

public partial class HocPhiChiTietModel2
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int StudentId { get; set; }

    public int HocPhiId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Content { get; set; }

    public int? AppID { get; set; }

    public int MaterHocPhiId { get; set; }

    public decimal Total { get; set; }

    public int? Quantity { get; set; }
    

    public virtual HocPhiModel2? HocPhi { get; set; }

    //public virtual MaterHocPhi? MaterHocPhi { get; set; }
}
