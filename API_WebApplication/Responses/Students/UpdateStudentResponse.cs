using API_WebApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace API_WebApplication.Responses.Students
{
    public class UpdateStudentResponse : BaseResponse
    {
        public Student Student { get; set; }
        //[Required]
        //public string Name { get; set; }

        //public bool IsCompleted { get; set; }
        //[Required]
        //public DateTime? UpdateDate { get; set; }
    }
}
