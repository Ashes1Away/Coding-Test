using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentRecords.Models;
using StudentRecords.WebApi.Storage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseRecords.WebApi
{
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly IReadOnly _readOnlyDb;
        private readonly IWriteOnly _writeOnlyDb;

        public CourseController(IWriteOnly writeOnly,
            IReadOnly readOnly)
        {
            _readOnlyDb = readOnly;
            _writeOnlyDb = writeOnly;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return _readOnlyDb.GetAllCourses();
        }

        // GET api/<controller>/5
        [HttpGet("{courseCode:alpha}")]
        public IEnumerable<Course> Get(string courseCode)
        {
            return _readOnlyDb.GetCourseById(courseCode);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(Course Course)
        {
            return Ok();
        }
    }
}
