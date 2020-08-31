using StudentRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecords.Web.Models
{
    public class AddEnrolmentModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public Enrolment Enrolment { get; set; }
        public IEnumerable<string> Students { get; set; }
        public string SelectedStudent { get; set; }
    }
}
