﻿namespace API_WebApplication.Requests
{
    public class StudentRequest
    {
        public string? NameStudent { get; set; }
        public string? Year1 { get; set; }
        public string? Class1 { get; set; }
        public string? Year2 { get; set; }
        public string? Class2 { get; set; }
        public string? Year3 { get; set; }
        public string? Class3 { get; set; }
        public string? GV1 { get; set; }
        public string? GV2 { get; set; }
        public string? GV3 { get; set; }

        public decimal? ChieuCao { get; set; }
        public decimal? CanNang { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        public bool GioiTinh { get; set; }

        public int? AppID { get; set; }
    }
}
