using System;

namespace StudentRecords.Models
{
    public class Enrolment
    {
        public string EnrolmentId { get; set; }
        public string AcademicYear { get; set; }
        public int YearOfStudy { get; set; }
        public string Occurrence { get; set; }
        public string ModeOfAttendance { get; set; }
        public string EnrolmentStatus { get; set; }
        public DateTime CourseEntryDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public Course Course { get; set; }
    }
}