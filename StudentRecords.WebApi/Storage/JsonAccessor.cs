using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentRecords.Models;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace StudentRecords.WebApi.Storage
{
    public class JsonAccessor: IReadOnly, IWriteOnly
    {
        private readonly string _studentJson = "Data\\students.json";
        private readonly string _courseJson = "Data\\Courses.json";
        private readonly IFileSystem _fileSystem;

        public JsonAccessor(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void AddEnrolmentInfo(Enrolment enrolment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            var json = _fileSystem.File.ReadAllText(_courseJson);
            return JArray.Parse(json).ToObject<IEnumerable<Course>>();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            var json = _fileSystem.File.ReadAllText(_studentJson);
            return JArray.Parse(json).ToObject<IEnumerable<Student>>();
        }

        public IEnumerable<Course> GetCourseById(string CourseCode)
        {
            var res = GetAllCourses();
            return res.Where(x => x.CourseCode == CourseCode);
        }

        public IEnumerable<Student> GetStudentById(int Id)
        {
            var res = GetAllStudents();
            return res.Where(x => x.StudentId == Id);
        }

        public void Upsert(Student student)
        {
            var allStudents = GetAllStudents();
            if(student.StudentId != 0 )
            {
                var index = allStudents.ToList().IndexOf(allStudents.Where(x => x.StudentId == student.StudentId).First());
                var allStudentsList = allStudents.ToList();
                allStudentsList[index] = student;
                allStudents = allStudentsList;
            }
            else
            {
                var id = allStudents.Select(x => x.StudentId).Max();
                id++;
                student.DisplayName = $"{student.FirstName} {student.LastName}";
                student.UniversityEmail = $"{student.FirstName}.{student.LastName}@mail.bcu.ac.uk";
                student.StudentId = id;
                student.NetworkId = "S" + id.ToString();
                allStudents = allStudents.ToList().Append(student);
            }
            SaveToJson(allStudents, _studentJson);
        }

        private void SaveToJson(object objectToSave, string Filename)
        {
            string output = JsonConvert.SerializeObject(objectToSave, Formatting.Indented);
            _fileSystem.File.WriteAllText(Filename, output);
        }
    }
}
