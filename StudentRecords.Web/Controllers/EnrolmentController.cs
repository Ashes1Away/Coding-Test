using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentRecords.Web.Models;
using StudentRecords.Web.Orchestrator;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentRecords.Web.Controllers
{
    public class EnrolmentController : Controller
    {
        private readonly IApiOrchestrator _orchestrator;

        public EnrolmentController(IApiOrchestrator apiOrchestrator)
        {
            _orchestrator = apiOrchestrator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewCourse()
        {
            var result = await _orchestrator.GetCourse();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _orchestrator.GetStudent(id.ToString());
            return View(result.Select(x => x.CourseEnrolment).First());
        }

        [HttpGet]
        public async Task<IActionResult> AddEnrolment()
        {
            var courses = await _orchestrator.GetCourse();
            var students = await _orchestrator.GetStudent();
            var model = new AddEnrolmentModel
            {
                Courses = courses,
                Students = students.Select(x => x.DisplayName).OrderBy(x => x)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEnrolment(AddEnrolmentModel model)
        {
            var students = await _orchestrator.GetStudent();
            var courses = await _orchestrator.GetCourse();
            var selectedStudent = students.Where(x => x.DisplayName == model.SelectedStudent).First();
            model.Enrolment.Course.CourseCode = courses.Where(x => x.CourseName == model.Enrolment.Course.CourseName).Select(x => x.CourseCode).FirstOrDefault();
            var temp = selectedStudent.CourseEnrolment.Select(x => Convert.ToInt32(x.EnrolmentId.Last().ToString())).Max();
            temp++;
            model.Enrolment.EnrolmentId = $"{selectedStudent.StudentId}/{temp}";
            selectedStudent.CourseEnrolment.Append(model.Enrolment);
            var result = await _orchestrator.Post(selectedStudent, "Students");
            return RedirectToAction("Details","Enrolment",new { id = selectedStudent.StudentId });
        }
    }
}
