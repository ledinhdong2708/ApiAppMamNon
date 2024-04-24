using API_WebApplication.Models;

namespace API_WebApplication.Responses.Students
{
    public class GetStudentDropdownResponse : BaseResponse
    {
        public List<StudentDropdown>? Students { get; set; }
    }

    public class StudentDropdown
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public decimal? ChieuCao { get; set; }
        public decimal? CanNang { get; set; }
    }
}
