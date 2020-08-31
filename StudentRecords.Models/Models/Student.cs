using System;
using System.Collections.Generic;

namespace StudentRecords.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string UniversityEmail { get; set; }
        public string NetworkId { get; set; }
        public string HomeOrOverseas { get; set; }
        public IEnumerable<Enrolment> CourseEnrolment { get; set; } = new List<Enrolment>();
    }
}
