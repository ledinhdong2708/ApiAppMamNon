using API_WebApplication.Controllers.Bases;
using API_WebApplication.Interfaces.Students;
using API_WebApplication.Models;
using API_WebApplication.Requests;
using API_WebApplication.Requests.Students;
using API_WebApplication.Responses.Students;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_WebApplication.Controllers.Students
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseApiController
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        //[Authorize(Roles = "1,2")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getStudentsResponse = await _studentService.GetStudentsByUser(UserID_Protected);
            if (!getStudentsResponse.Success)
            {
                return UnprocessableEntity(getStudentsResponse);
            }

            var tasksResponse = getStudentsResponse.Students.ConvertAll(o => new Student
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                NameStudent = o.NameStudent,
                Year1 = o.Year1,
                Year2 = o.Year2,
                Year3 = o.Year3,
                Class1 = o.Class1,
                Class2 = o.Class2,
                Class3 = o.Class3,
                GV1 = o.GV1,
                GV2 = o.GV2,
                GV3 = o.GV3,
                CanNang = o.CanNang,
                ChieuCao = o.ChieuCao,
                CreateDate = o.CreateDate,
                img = o.img,
                imagePatch = o.imagePatch,
                GioiTinh = o.GioiTinh,
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("bykhoahocclass")]
        public async Task<IActionResult> GetAllFillterByUserKhoaHocClass(string khoaHocId, string classId)
        {
            var getStudentsResponse = await _studentService.GetStudentsFillterByUserKhoaHocClass(UserID_Protected, khoaHocId, classId);
            if (!getStudentsResponse.Success)
            {
                return UnprocessableEntity(getStudentsResponse);
            }

            var tasksResponse = getStudentsResponse.Students.ConvertAll(o => new Student
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                NameStudent = o.NameStudent,
                Year1 = o.Year1,
                Year2 = o.Year2,
                Year3 = o.Year3,
                Class1 = o.Class1,
                Class2 = o.Class2,
                Class3 = o.Class3,
                GV1 = o.GV1,
                GV2 = o.GV2,
                GV3 = o.GV3,
                CanNang = o.CanNang,
                ChieuCao = o.ChieuCao,
                CreateDate = o.CreateDate,
                img = o.img,
                imagePatch = o.imagePatch,
                GioiTinh = o.GioiTinh
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("bykhoahocclassdate")]
        public async Task<IActionResult> GetAllFillterByUserKhoaHocClassDate(string khoaHocId, string classId, string day, string month, string year)
        {
            var getStudentsResponse = await _studentService.GetStudentsFillterByUserKhoaHocClassDate(UserID_Protected, khoaHocId, classId, day, month, year);
            if (!getStudentsResponse.Success)
            {
                return UnprocessableEntity(getStudentsResponse);
            }

            //var tasksResponse = getStudentsResponse.Students.ConvertAll(o => new Student
            //{
            //    Id = o.Id,
            //    IsCompleted = o.IsCompleted,
            //    NameStudent = o.NameStudent,
            //    Year1 = o.Year1,
            //    Year2 = o.Year2,
            //    Year3 = o.Year3,
            //    Class1 = o.Class1,
            //    Class2 = o.Class2,
            //    Class3 = o.Class3,
            //    GV1 = o.GV1,
            //    GV2 = o.GV2,
            //    GV3 = o.GV3,
            //    CanNang = o.CanNang,
            //    ChieuCao = o.ChieuCao,
            //    CreateDate = o.CreateDate,
            //    img = o.img,
            //    imagePatch = o.imagePatch,
            //    GioiTinh = o.GioiTinh
            //});
            return Ok(getStudentsResponse);
        }

        //[Authorize(Roles = "1,2")]
        [Authorize]
        [HttpGet("byId")]
        public async Task<IActionResult> GetByID(int StudentID)
        {
            var getStudentResponse = await _studentService.GetIDStudent(UserID_Protected, StudentID);
            if (!getStudentResponse.Success)
            {
                return UnprocessableEntity(getStudentResponse);
            }

            var tasksResponse = new Student
            {
                Id = getStudentResponse.Student.Id,
                IsCompleted = getStudentResponse.Student.IsCompleted,
                NameStudent = getStudentResponse.Student.NameStudent,
                Year1 = getStudentResponse.Student.Year1,
                Year2 = getStudentResponse.Student.Year2,
                Year3 = getStudentResponse.Student.Year3,
                Class1 = getStudentResponse.Student.Class1,
                Class2 = getStudentResponse.Student.Class2,
                Class3 = getStudentResponse.Student.Class3,
                GV1 = getStudentResponse.Student.GV1,
                GV2 = getStudentResponse.Student.GV2,
                GV3 = getStudentResponse.Student.GV3,
                CreateDate = getStudentResponse.Student.CreateDate,
                img = getStudentResponse.Student.img,
                imagePatch = getStudentResponse.Student.imagePatch,
                GioiTinh = getStudentResponse.Student.GioiTinh
            };
            return Ok(tasksResponse);
        }

        //[Authorize(Roles = "1,2")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(StudentRequest studentRequest)
        {
            DateTime dateTime = DateTime.Now;
            var student = new Student
            {
                Year1 = studentRequest.Year1,
                Year2 = studentRequest.Year2,
                Year3 = studentRequest.Year3,
                Class1 = studentRequest.Class1,
                Class2 = studentRequest.Class2,
                Class3 = studentRequest.Class3,
                GV1 = studentRequest.GV1,
                GV2 = studentRequest.GV2,
                GV3 = studentRequest.GV3,
                IsCompleted = studentRequest.IsCompleted,
                CreateDate = dateTime,
                Role = 0,
                NameStudent = studentRequest.NameStudent,
                UserId = UserID_Protected,
                ChieuCao = studentRequest.ChieuCao,
                CanNang = studentRequest.CanNang,
                GioiTinh = studentRequest.GioiTinh,
                AppID = studentRequest.AppID,
            };
            var saveStudentResponse = await _studentService.SaveStudent(student);
            if (!saveStudentResponse.Success)
            {
                return UnprocessableEntity(saveStudentResponse);
            }
            var taskResponse = new Student
            {
                Id = saveStudentResponse.Student.Id,
                IsCompleted = saveStudentResponse.Student.IsCompleted,
                NameStudent = saveStudentResponse.Student.NameStudent,
                Year1 = saveStudentResponse.Student.Year1,
                Year2 = saveStudentResponse.Student.Year2,
                Year3 = saveStudentResponse.Student.Year3,
                Class1 = saveStudentResponse.Student.Class1,
                Class2 = saveStudentResponse.Student.Class2,
                Class3 = saveStudentResponse.Student.Class3,
                GV1 = saveStudentResponse.Student.GV1,
                GV2 = saveStudentResponse.Student.GV2,
                GV3 = saveStudentResponse.Student.GV3,
                CreateDate = saveStudentResponse.Student.CreateDate,
                CanNang = saveStudentResponse.Student.CanNang,
                ChieuCao = saveStudentResponse.Student.CanNang,
                GioiTinh = saveStudentResponse.Student.GioiTinh,
                AppID = saveStudentResponse.Student.AppID,
            };

            return Ok(taskResponse);
        }

        //[Authorize(Roles = "1")]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteStudentResponse = await _studentService.DeleteStudent(id, UserID_Protected);
            if (!deleteStudentResponse.Success)
            {
                return UnprocessableEntity(deleteStudentResponse);
            }
            return Ok(deleteStudentResponse.StudentId);
        }

        //[Authorize(Roles = "1,2")]
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, StudentRequest studentUpdateRequest)
        {
            DateTime dateTime = DateTime.Now;
            var student = new Student
            {
                IsCompleted = studentUpdateRequest.IsCompleted,
                UpdateDate = dateTime,
                NameStudent = studentUpdateRequest.NameStudent,
                Year1 = studentUpdateRequest.Year1,
                Year2 = studentUpdateRequest.Year2,
                Year3 = studentUpdateRequest.Year3,
                Class1 = studentUpdateRequest.Class1,
                Class2 = studentUpdateRequest.Class2,
                Class3 = studentUpdateRequest.Class3,
                GV1 = studentUpdateRequest.GV1,
                GV2 = studentUpdateRequest.GV2,
                GV3 = studentUpdateRequest.GV3,
                UserId = UserID_Protected,
                ChieuCao = studentUpdateRequest.ChieuCao,
                CanNang = studentUpdateRequest.CanNang,
                GioiTinh = studentUpdateRequest.GioiTinh,
                AppID = studentUpdateRequest.AppID,
            };
            var saveStudentResponse = await _studentService.UpdateStudent(id, student);
            if (!saveStudentResponse.Success)
            {
                return UnprocessableEntity(saveStudentResponse);
            }
            var taskResponse = new Student
            {
                IsCompleted = saveStudentResponse.Student.IsCompleted,
                NameStudent = saveStudentResponse.Student.NameStudent,
                Year1 = saveStudentResponse.Student.Year1,
                Year2 = saveStudentResponse.Student.Year2,
                Year3 = saveStudentResponse.Student.Year3,
                Class1 = saveStudentResponse.Student.Class1,
                Class2 = saveStudentResponse.Student.Class2,
                Class3 = saveStudentResponse.Student.Class3,
                GV1 = saveStudentResponse.Student.GV1,
                GV2 = saveStudentResponse.Student.GV2,
                GV3 = saveStudentResponse.Student.GV3,
                ChieuCao = saveStudentResponse.Student.ChieuCao,
                CanNang = saveStudentResponse.Student.CanNang,
                UpdateDate = dateTime,
                img = saveStudentResponse.Student.img,
                imagePatch = saveStudentResponse.Student.imagePatch,
                GioiTinh = saveStudentResponse.Student.GioiTinh,
                AppID = saveStudentResponse.Student.AppID,
            };

            return Ok(taskResponse);
        }

        [Authorize]
        [HttpGet("{classiD}")]
        public async Task<IActionResult> GetAllStudentInClass(string classiD)
        {
            var getStudentsResponse = await _studentService.GetAllStudentInClass(classiD, UserID_Protected);
            if (!getStudentsResponse.Success)
            {
                return UnprocessableEntity(getStudentsResponse);
            }

            var tasksResponse = getStudentsResponse.Students.ConvertAll(o => new StudentDropdown
            {
                Id = o.Id,
                Name = o.NameStudent,
                ChieuCao = (decimal?)o.ChieuCao,
                CanNang = (decimal?)o.CanNang
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpGet("classname/{nameclass}")]
        public async Task<IActionResult> GetAllStudentInNameClass(string nameClass)
        {
            var getStudentsResponse = await _studentService.GetAllStudentInNameClass(nameClass, UserID_Protected);
            if (!getStudentsResponse.Success)
            {
                return UnprocessableEntity(getStudentsResponse);
            }

            var tasksResponse = getStudentsResponse.Students.ConvertAll(o => new Student
            {
                Id = o.Id,
                IsCompleted = o.IsCompleted,
                NameStudent = o.NameStudent,
                Year1 = o.Year1,
                Year2 = o.Year2,
                Year3 = o.Year3,
                Class1 = o.Class1,
                Class2 = o.Class2,
                Class3 = o.Class3,
                GV1 = o.GV1,
                GV2 = o.GV2,
                GV3 = o.GV3,
                CanNang = o.CanNang,
                ChieuCao = o.ChieuCao,
                CreateDate = o.CreateDate,
                imagePatch = o.imagePatch,
                img = o.img,
                GioiTinh = o.GioiTinh,
            });
            return Ok(tasksResponse);
        }

        [Authorize]
        [HttpPost("UploadFile")]
        //[RequestSizeLimit(1000 * 1024 * 1024 )]
        public async Task<IActionResult> UploadImageStudent(int studentId, [FromForm] UploadFileStudentModel fileStudentModel)
        {
            var uniqueFileName = Guid.NewGuid().ToString();
            var stringCutted = fileStudentModel.file.FileName.Split('.').Last();
            var fileStudent = new UploadFileStudentModel
            {
                //FileName = fileStudentModel.file.FileName,
                FileName = uniqueFileName+"."+ stringCutted,
                file = fileStudentModel.file
            };
            var saveStudentResponse = await _studentService.UploadImageStudent(studentId, fileStudent, UserID_Protected.ToString());
            if (!saveStudentResponse.Success)
            {
                return UnprocessableEntity(saveStudentResponse);
            }
            var taskResponse = new Student
            {
                IsCompleted = saveStudentResponse.Student.IsCompleted,
                NameStudent = saveStudentResponse.Student.NameStudent,
                Year1 = saveStudentResponse.Student.Year1,
                Year2 = saveStudentResponse.Student.Year2,
                Year3 = saveStudentResponse.Student.Year3,
                Class1 = saveStudentResponse.Student.Class1,
                Class2 = saveStudentResponse.Student.Class2,
                Class3 = saveStudentResponse.Student.Class3,
                GV1 = saveStudentResponse.Student.GV1,
                GV2 = saveStudentResponse.Student.GV2,
                GV3 = saveStudentResponse.Student.GV3,
                ChieuCao = saveStudentResponse.Student.ChieuCao,
                CanNang = saveStudentResponse.Student.CanNang,
                imagePatch = saveStudentResponse.Student.imagePatch,
                img = saveStudentResponse.Student.img,
                GioiTinh = saveStudentResponse.Student.GioiTinh,
            };

            return Ok(taskResponse);

        }
    }
}
