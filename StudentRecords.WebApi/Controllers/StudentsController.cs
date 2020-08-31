using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StudentRecords.Models;
using StudentRecords.WebApi.Storage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentRecords.WebApi
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {

        private readonly IReadOnly _readOnlyDb;
        private readonly IWriteOnly _writeOnlyDb;

        public StudentsController(IWriteOnly writeOnly,
            IReadOnly readOnly)
        {
            _readOnlyDb = readOnly;
            _writeOnlyDb = writeOnly;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _readOnlyDb.GetAllStudents();
        }

        // GET api/<controller>/5
        [HttpGet("{id:int}")]
        public IEnumerable<Student> Get(int id)
        {
            return _readOnlyDb.GetStudentById(id);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Student student)
        {
            try
            {
                _writeOnlyDb.Upsert(student);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
