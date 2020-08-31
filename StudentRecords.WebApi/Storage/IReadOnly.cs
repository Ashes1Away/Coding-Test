using StudentRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecords.WebApi.Storage
{
    public interface IReadOnly
    {
        IEnumerable<Student> GetAllStudents();
        IEnumerable<Student> GetStudentById(int Id);
        IEnumerable<Course> GetAllCourses();
        IEnumerable<Course> GetCourseById(string CourseCode);
    }
}
