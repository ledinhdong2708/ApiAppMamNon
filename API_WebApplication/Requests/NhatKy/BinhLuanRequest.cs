﻿namespace API_WebApplication.Requests.NhatKy
{
    public partial class BinhLuanRequest
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }

        public int NhatKyId { get; set; }

        public int UserId { get; set; }

        public int StudentId { get; set; }
        public int? AppID { get; set; }
    }
}
