using StudentRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StudentRecords.Web.Orchestrator
{
    public interface IApiOrchestrator
    {
        Task<IEnumerable<Course>> GetCourse(string endpoint = "");
        Task<IEnumerable<Student>> GetStudent(string endpoint = "");
        Task<HttpResponseMessage> Post(object objectToPost, string endpoint = "");
    }
}
