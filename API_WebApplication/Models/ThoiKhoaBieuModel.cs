using System;
using System.Collections.Generic;

namespace API_WebApplication.Models;

public partial class ThoiKhoaBieuModel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int Role { get; set; }

    public string? ClassTKB { get; set; }

    public string? NameTKB { get; set; }

    public string? Command { get; set; }

    public string? Time06300720 { get; set; }

    public string? Time07200730 { get; set; }

    public string? Time07300815 { get; set; }

    public string? Time08150845 { get; set; }

    public string? Time08450900 { get; set; }

    public string? Time09000930 { get; set; }

    public string? Time09301015 { get; set; }

    public string? Time10151115 { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool IsCompleted { get; set; }

    public int? KhoaHocId { get; set; }

    public int? AppID { get; set; }

    public string? Time11151400 { get; set; }

    public string? Time14001415 { get; set; }

    public string? Time14151500 { get; set; }

    public string? Time15001515 { get; set; }

    public string? Time15151540 { get; set; }

    public string? Time15301630 { get; set; }

    public string? Time16301730 { get; set; }

    public string? Time17301815 { get; set; }
    public string? days { get; set; }
    public string? months { get; set; }
    public string? years { get; set; }
}
