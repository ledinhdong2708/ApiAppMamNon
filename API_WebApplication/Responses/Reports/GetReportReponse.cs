using API_WebApplication.Models;
using API_WebApplication.Requests.Reports;

namespace API_WebApplication.Responses.Reports
{
    public class GetReportReponse : BaseResponse
    {
        public List<ReportDiemDanhRequest> ReportDiemDanhRequests { get; set; }
    }
}
