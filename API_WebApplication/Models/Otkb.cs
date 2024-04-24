using System;
using System.Collections.Generic;

namespace API_WebApplication.Models;

public partial class OTKB
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool Status { get; set; }

    public int UserId { get; set; }

    public int StudentId { get; set; }

    public int ParentId { get; set; }

    public int Branch { get; set; }

    public virtual ICollection<TKB1> TKB1s { get; set; } = new List<TKB1>();
}
