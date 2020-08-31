using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentRecords.Models;
using StudentRecords.Web.Orchestrator;

namespace StudentRecords.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IApiOrchestrator _orchestrator;

        public StudentController(IApiOrchestrator apiOrchestrator)
        {
            _orchestrator = apiOrchestrator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Student()
        {
            var data = await _orchestrator.GetStudent();
            return View(data);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _orchestrator.GetStudent(id.ToString());
            return View("AddStudent",student.First());
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            var result = await _orchestrator.Post(student, "Students");
            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("Student");
            }
            else
            {
                return View();
            }
        }
    }
}