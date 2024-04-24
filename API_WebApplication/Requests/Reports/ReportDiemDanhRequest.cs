using API_WebApplication.Models;

namespace API_WebApplication.Requests.Reports
{
    public class ReportDiemDanhRequest
    {
        public int StudentId { get; set; }

        public int SoNgayHoc { get; set; }

        public int SoNgayNghi { get; set; }
        public Student Student { get; set; }

        public int? AppID { get; set; }
    }
}
