using API_WebApplication.Interfaces.Students;
using API_WebApplication.Models;
using API_WebApplication.Requests.Students;
using API_WebApplication.Responses.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace API_WebApplication.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly API_Application_V1Context _aPI_Application_V1Context;
        public StudentService(API_Application_V1Context aPI_Application_V1Context)
        {
            this._aPI_Application_V1Context = aPI_Application_V1Context;
        }

        public async Task<DeleteStudentResponse> DeleteStudent(int studentId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var student = await _aPI_Application_V1Context.Students.Where(o => o.Id == studentId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var student = await _aPI_Application_V1Context.Students.FindAsync(studentId);
            if (student == null)
            {
                return new DeleteStudentResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }
            if (student.UserId != userId)
            {
                return new DeleteStudentResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }
            _aPI_Application_V1Context.Students.Remove(student);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();
            if (saveResponse >= 0)
            {
                return new DeleteStudentResponse
                {
                    Success = true,
                    StudentId = student.Id
                };
            }
            return new DeleteStudentResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }
        public async Task<StudentResponse> GetIDStudent(int userId, int studentId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var student = await _aPI_Application_V1Context.Students.Where(o => o.Id == studentId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var student = await _aPI_Application_V1Context.Students.FindAsync(studentId);
            if (student == null)
            {
                return new StudentResponse
                {
                    Success = false,
                    Error = "Student not found",
                    ErrorCode = "T01"
                };
            }
            if (student.UserId != userId)
            {
                return new StudentResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this task",
                    ErrorCode = "T02"
                };
            }
            if (student.Id < 0)
            {
                return new StudentResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new StudentResponse
            { 
                Success = true,
                Student = student 
            };
        }
        public async Task<GetStudentsResponse> GetStudentsByUser(int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var student = await _aPI_Application_V1Context.Students.Where(o => o.UserId == userId && o.AppID == appID!.AppID).OrderByDescending(o => o.CreateDate).ToListAsync();
            //if (student.Count == 0)
            //{
            //    return new GetStudentsResponse
            //    {
            //        Success = false,
            //        Error = "No Student found for this user",
            //        ErrorCode = "T04",
            //        Data = student.ToList()
            //    };
            //}
            return new GetStudentsResponse { Success = true, Students = student.ToList() };
        }
        public async Task<GetStudentsResponse> GetStudentsFillterByUserKhoaHocClass(int userId, string khoaHocId, string classId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            //var student_check = await _aPI_Application_V1Context.Students
            //    .Where(o =>o.Year1 == khoaHocId && o.Class1 == classId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //if(student_check == null)
            //{
            //    return new GetStudentsResponse
            //    {
            //        Success = false,
            //        Error = "Student not found",
            //        ErrorCode = "T01"
            //    };
            //}

            //_aPI_Application_V1Context.Users.Where(_ => _.Id.ToString() == o.GV1).FirstOrDefault()!.FirstName!.ToString()
            //            + " - " + _aPI_Application_V1Context.Users.Where(_ => _.Id.ToString() == o.GV1).FirstOrDefault()!.LastName!.ToString()
            var student = await _aPI_Application_V1Context.Students
                .Where(
                o => 
                //o.UserId == userId && 
                o.Year1 == khoaHocId && o.Class1 == classId && o.AppID == appID!.AppID
                )
                .Select(o=> new Student
                {
                    Id = o.Id,
                    IsCompleted = o.IsCompleted,
                    NameStudent = o.NameStudent,
                    Year1 = _aPI_Application_V1Context.KhoaHocModels.Where(_ => _.Id.ToString() == o.Year1).FirstOrDefault()!.FromYear.ToString()
                        + " - " + _aPI_Application_V1Context.KhoaHocModels.Where(_ => _.Id.ToString() == o.Year1).FirstOrDefault()!.ToYear.ToString(),
                    Year2 = o.Year2,
                    Year3 = o.Year3,
                    Class1 = _aPI_Application_V1Context.ClassModels.Where(_ => _.Id.ToString() == o.Class1).FirstOrDefault()!.NameClass!.ToString(),
                    Class2 = o.Class2,
                    Class3 = o.Class3,
                    GV1 = "",
                    GV2 = o.GV2,
                    GV3 = o.GV3,
                    CanNang = o.CanNang,
                    ChieuCao = o.ChieuCao,
                    CreateDate = o.CreateDate,
                    img = o.img,
                    imagePatch = o.imagePatch,
                    GioiTinh = o.GioiTinh,
                }).OrderByDescending(o => o.CreateDate)
                .ToListAsync();
            //if (student.Count == 0)
            //{
            //    return new GetStudentsResponse
            //    {
            //        Success = false,
            //        Error = "No Student found for this user",
            //        ErrorCode = "T04",
            //        Data = student.ToList()
            //    };
            //}
            return new GetStudentsResponse { Success = true, Students = student.ToList() };
        }
        public async Task<GetStudentsDiemDanhResponse> GetStudentsFillterByUserKhoaHocClassDate(int userId, string khoaHocId, string classId, string day, string month, string year)
        {

            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();

            var xinNghiPheps = await _aPI_Application_V1Context.XinNghiPhepModels
               .Where(x => x.Id != null)
               .Include(x => x.ChiTietXinNghiPheps)
               .ToListAsync();

            DateTime now = DateTime.Now;
            foreach (var xinNghiPhep in xinNghiPheps)
            {
                var diemdanhInMonthYear = await _aPI_Application_V1Context.DiemDanhModels
                    .Where(s => s.StudentId == xinNghiPhep.StudentId)
                    .ToListAsync();

                foreach (var chiTiet in xinNghiPhep.ChiTietXinNghiPheps)
                {
                    for (DateTime date = chiTiet.FromDate; date <= chiTiet.ToDate; date = date.AddDays(1))
                    {
                        var dateMonthYear = $"{date.Month}/{date.Year}";

                        var diemDanhForDate = diemdanhInMonthYear.FirstOrDefault(d => $"{d.CreateDate.Month}/{d.CreateDate.Year}" == dateMonthYear && d.CreateDate.Day == date.Day && d.DenLop != true);
                        if (diemDanhForDate != null )
                        {
                            diemDanhForDate.CoPhep = true;
                        }
                        else
                        {
                            if (date.Date == now.Date)
                            {
                                // Kiểm tra xem học sinh đã được điểm danh trong ngày hiện tại chưa
                                var existingDiemDanh = await _aPI_Application_V1Context.DiemDanhModels.FirstOrDefaultAsync(d => d.StudentId == chiTiet.StudentId && d.CreateDate.Date == date.Date);
                                if (existingDiemDanh == null)
                                {
                                    // Nếu chưa, thêm mới bản ghi điểm danh
                                    var newDiemDanh = new DiemDanhModel
                                    {
                                        CreateDate = DateTime.Now, 
                                        CoPhep = true, 
                                        StudentId = chiTiet.StudentId,
                                        Role = 0,
                                        UserId = 1,
                                        AppID = 0,
                                        IsCompleted = false,
                                        KhongPhep = false,
                                        DenLop = false,
                                        Content = chiTiet.Content,
                                    };
                                    _aPI_Application_V1Context.DiemDanhModels.Add(newDiemDanh);
                                }
                                else
                                {
                                    diemDanhForDate.CoPhep = true;
                                }
                            }
                            if(date.Date < now.Date)
                            {
                                // Kiểm tra xem học sinh đã được điểm danh trong ngày hiện tại chưa
                                var existingDiemDanh = await _aPI_Application_V1Context.DiemDanhModels.FirstOrDefaultAsync(d => d.StudentId == chiTiet.StudentId && d.CreateDate == date);
                                if (existingDiemDanh == null)
                                {
                                    // Nếu chưa, thêm mới bản ghi điểm danh
                                    var newDiemDanh = new DiemDanhModel
                                    {
                                        CreateDate = date, // Đặt ngày tạo mới
                                        CoPhep = true, // Đặt trạng thái điểm danh thành true
                                        StudentId = chiTiet.StudentId,
                                        Role = 0,
                                        UserId = 1,
                                        AppID = 0,
                                        IsCompleted = false,
                                        KhongPhep = false,
                                        DenLop = false,
                                        Content = chiTiet.Content,
                                    };
                                    _aPI_Application_V1Context.DiemDanhModels.Add(newDiemDanh);
                                }
                            }
                        }
                    }
                }
            }

            await _aPI_Application_V1Context.SaveChangesAsync();


            var diemdanh = await _aPI_Application_V1Context.DiemDanhModels.Where(o => o.AppID == appID!.AppID
                && o.CreateDate.Day.ToString() == day
                && o.CreateDate.Month.ToString() == month
                && o.CreateDate.Year.ToString() == year
            ).Join(_aPI_Application_V1Context.Students, p => p.StudentId, c => c.Id, (p, c) => new { p, c })
                    .Where(_ =>
                    _.c.Class1 == classId
                    && _.c.Year1 == khoaHocId
                    && _.c.AppID == appID!.AppID).ToListAsync();
            if (diemdanh.Count == 0)
            {
                var result = await _aPI_Application_V1Context.Students
                .Where(
                o =>
                o.Year1 == khoaHocId && o.Class1 == classId && o.AppID == appID!.AppID
                )
                .Select(o => new StudentDiemDanh
                {
                    Id = o.Id,
                    IsCompleted = o.IsCompleted,
                    NameStudent = o.NameStudent,
                    Year1 = _aPI_Application_V1Context.KhoaHocModels.Where(_ => _.Id.ToString() == o.Year1).FirstOrDefault()!.FromYear.ToString()
                        + " - " + _aPI_Application_V1Context.KhoaHocModels.Where(_ => _.Id.ToString() == o.Year1).FirstOrDefault()!.ToYear.ToString(),
                    Year2 = o.Year2,
                    Year3 = o.Year3,
                    Class1 = _aPI_Application_V1Context.ClassModels.Where(_ => _.Id.ToString() == o.Class1).FirstOrDefault()!.NameClass!.ToString(),
                    Class2 = o.Class2,
                    Class3 = o.Class3,
                    GV1 = _aPI_Application_V1Context.Users.Where(_ => _.Id.ToString() == o.GV1).FirstOrDefault()!.FirstName!.ToString()
                        + " - " + _aPI_Application_V1Context.Users.Where(_ => _.Id.ToString() == o.GV1).FirstOrDefault()!.LastName!.ToString(),
                    GV2 = o.GV2,
                    GV3 = o.GV3,
                    CanNang = o.CanNang,
                    ChieuCao = o.ChieuCao,
                    CreateDate = o.CreateDate,
                    img = o.img,
                    imagePatch = o.imagePatch,
                    GioiTinh = o.GioiTinh
                }).OrderByDescending(o => o.CreateDate)
                .ToListAsync();
                return new GetStudentsDiemDanhResponse
                {
                    Success = true,
                    StudentDiemDanhs = result.ToList()
                };
            }
            else
            {
                var result = await _aPI_Application_V1Context.Students
                 .Where(s => s.Class1 == classId && s.Year1 == khoaHocId && s.AppID == appID!.AppID)
                 .GroupJoin(
                     _aPI_Application_V1Context.DiemDanhModels.Where(d =>
                         d.CreateDate.Day.ToString() == day &&
                         d.CreateDate.Month.ToString() == month &&
                         d.CreateDate.Year.ToString() == year &&
                         d.AppID == appID!.AppID),
                     student => student.Id,
                     diemDanh => diemDanh.StudentId,
                     (student, diemDanhGroup) => new { Student = student, DiemDanhGroup = diemDanhGroup })
                 .SelectMany(
                     x => x.DiemDanhGroup.DefaultIfEmpty(),
                     (student, diemDanh) => new StudentDiemDanh
                     {
                         Id = student.Student.Id,
                         IsCompleted = student.Student.IsCompleted,
                         NameStudent = student.Student.NameStudent,
                         Year1 = _aPI_Application_V1Context.KhoaHocModels
                             .Where(kh => kh.Id.ToString() == student.Student.Year1)
                             .Select(kh => $"{kh.FromYear} - {kh.ToYear}")
                             .FirstOrDefault(),
                         Year2 = student.Student.Year2,
                         Year3 = student.Student.Year3,
                         Class1 = _aPI_Application_V1Context.ClassModels
                             .Where(c => c.Id.ToString() == student.Student.Class1)
                             .Select(c => c.NameClass)
                             .FirstOrDefault(),
                         Class2 = student.Student.Class2,
                         Class3 = student.Student.Class3,
                         GV1 = _aPI_Application_V1Context.Users
                             .Where(u => u.Id.ToString() == student.Student.GV1)
                             .Select(u => $"{u.FirstName} - {u.LastName}")
                             .FirstOrDefault(),
                         GV2 = student.Student.GV2,
                         GV3 = student.Student.GV3,
                         CanNang = student.Student.CanNang,
                         ChieuCao = student.Student.ChieuCao,
                         img = student.Student.img,
                         imagePatch = student.Student.imagePatch,
                         GioiTinh = student.Student.GioiTinh,
                         DenLop = diemDanh != null ? diemDanh.DenLop : default,
                         KhongPhep = diemDanh != null ? diemDanh.KhongPhep : default,
                         CoPhep = diemDanh != null ? diemDanh.CoPhep : default,
                         idDiemDanh = diemDanh != null ? diemDanh.Id : default
                     })
                 .Distinct()
                 .ToListAsync();


                return new GetStudentsDiemDanhResponse { Success = true, StudentDiemDanhs = result.ToList() };
            }
        }



        //var result = await _aPI_Application_V1Context.Students
        //.Where(
        //o =>
        ////o.UserId == userId && 
        //o.Year1 == khoaHocId && o.Class1 == classId && o.AppID == appID!.AppID
        //)
        //.Select(o => new StudentDiemDanh
        //{
        //    Id = o.Id,
        //    IsCompleted = o.IsCompleted,
        //    NameStudent = o.NameStudent,
        //    Year1 = _aPI_Application_V1Context.KhoaHocModels.Where(_ => _.Id.ToString() == o.Year1).FirstOrDefault()!.FromYear.ToString()
        //        + " - " + _aPI_Application_V1Context.KhoaHocModels.Where(_ => _.Id.ToString() == o.Year1).FirstOrDefault()!.ToYear.ToString(),
        //    Year2 = o.Year2,
        //    Year3 = o.Year3,
        //    Class1 = _aPI_Application_V1Context.ClassModels.Where(_ => _.Id.ToString() == o.Class1).FirstOrDefault()!.NameClass!.ToString(),
        //    Class2 = o.Class2,
        //    Class3 = o.Class3,
        //    GV1 = _aPI_Application_V1Context.Users.Where(_ => _.Id.ToString() == o.GV1).FirstOrDefault()!.FirstName!.ToString()
        //        + " - " + _aPI_Application_V1Context.Users.Where(_ => _.Id.ToString() == o.GV1).FirstOrDefault()!.LastName!.ToString(),
        //    GV2 = o.GV2,
        //    GV3 = o.GV3,
        //    CanNang = o.CanNang,
        //    ChieuCao = o.ChieuCao,
        //    CreateDate = o.CreateDate,
        //    img = o.img,
        //    imagePatch = o.imagePatch,
        //    GioiTinh = o.GioiTinh,
        //}).LeftJoin().OrderByDescending(o => o.CreateDate)
        //.ToListAsync();
        public async Task<SaveStudentResponse> SaveStudent(Student student)
        {
            await _aPI_Application_V1Context.Students.AddAsync(student);
            var saveResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new SaveStudentResponse
                {
                    Success = true,
                    Student = student
                };
            }
            return new SaveStudentResponse
            {
                Success = false,
                Error = "Unable to save student",
                ErrorCode = "T05"
            };
        }
        public async Task<UpdateStudentResponse> UpdateStudent(int studentId, Student student)
        {
            var studentById = await _aPI_Application_V1Context.Students.FindAsync(studentId);
            if (studentById == null)
            {
                return new UpdateStudentResponse
                {
                    Success = false,
                    Error = "Student not found",
                    ErrorCode = "T01"
                };
            }
            if (student.UserId != studentById.UserId)
            {
                return new UpdateStudentResponse
                {
                    Success = false,
                    Error = "You don't have access to get id this student",
                    ErrorCode = "T02"
                };
            }

            studentById.NameStudent = student.NameStudent;
            studentById.Year1 = student.Year1;
            studentById.Year2 = student.Year2;
            studentById.Year3 = student.Year3;
            studentById.Class1 = student.Class1;
            studentById.Class2 = student.Class2;
            studentById.Class3 = student.Class3;
            studentById.GV1 = student.GV1;
            studentById.GV2 = student.GV2;
            studentById.GV3 = student.GV3;
            studentById.ChieuCao = student.ChieuCao;
            studentById.CanNang = student.CanNang;
            studentById.UpdateDate = student.UpdateDate;
            studentById.IsCompleted = student.IsCompleted;
            studentById.GioiTinh = student.GioiTinh;
            studentById.AppID = student.AppID;

            // Upload Image

            // End Upload Image

            var updateResponse = await _aPI_Application_V1Context.SaveChangesAsync();

            if (updateResponse >= 0)
            {
                return new UpdateStudentResponse
                {
                    Success = true,
                    Student = student
                };
            }
            return new UpdateStudentResponse
            {
                Success = false,
                Error = "Unable to update task",
                ErrorCode = "T05"
            };
        }
        public async Task<GetStudentsResponse> GetAllStudentInClass(string classID, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var student = await _aPI_Application_V1Context.Students.Where(o => 
                o.Class1 == classID && o.AppID == appID!.AppID && o.IsCompleted == false
            ).OrderByDescending(o => o.CreateDate).ToListAsync();

            return new GetStudentsResponse { Success = true, Students = student.ToList() };
        }

        public async Task<StudentResponse> GetIDByStudent(int studentId, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var student = await _aPI_Application_V1Context.Students.Where(o => o.Id == studentId && o.AppID == appID!.AppID).FirstOrDefaultAsync();
            //var student = await _aPI_Application_V1Context.Students.FindAsync(studentId);
            if (student == null)
            {
                return new StudentResponse
                {
                    Success = false,
                    Error = "Student not found",
                    ErrorCode = "T01"
                };
            }
            if (student.Id < 0)
            {
                return new StudentResponse
                {
                    Success = false,
                    Error = "No Student found for this user",
                    ErrorCode = "T04"
                };
            }
            return new StudentResponse
            {
                Success = true,
                Student = student
            };
        }

        public async Task<GetStudentsResponse> GetAllStudentInNameClass(string nameClass, int userId)
        {
            var appID = await _aPI_Application_V1Context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
            var student = await _aPI_Application_V1Context.Students.Where(o =>
            o.Class1 == nameClass && o.AppID == appID!.AppID
            //o.Class1!.Contains(nameClass) || o.Class2!.Contains(nameClass) || o.Class3!.Contains(nameClass)
            ).OrderByDescending(o => o.CreateDate).ToListAsync();

            return new GetStudentsResponse { Success = true, Students = student.ToList() };
        }

        public async Task<UpdateStudentResponse> UploadImageStudent(int studentId,[FromForm] UploadFileStudentModel fileStudentModel, string idUser)
        {
                var studentById = await _aPI_Application_V1Context.Students.FindAsync(studentId);
                if (studentById == null)
                {
                    return new UpdateStudentResponse
                    {
                        Success = false,
                        Error = "Student not found",
                        ErrorCode = "T01"
                    };
                }

                //string root = @"C:\imageAPIApp\" + idUser;
                string root = @"upload\images" + idUser;
                // If directory does not exist, create it.
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string patch = Path.Combine(root, fileStudentModel.FileName);
                //string patch = Path.Combine(root, fileStudentModel.FileName);
                using (Stream stream = new FileStream(patch, FileMode.Create))
                {
                    await fileStudentModel.file.CopyToAsync(stream);
                    stream.Close();

                }
                studentById.img = fileStudentModel.FileName;
                studentById.imagePatch = patch;
                await _aPI_Application_V1Context.SaveChangesAsync();
                return new UpdateStudentResponse
                {
                    Success = true,
                    Student = studentById
                };
        }
    }
}
