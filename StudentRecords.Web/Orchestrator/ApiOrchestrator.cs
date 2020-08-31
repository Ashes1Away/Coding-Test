using Newtonsoft.Json;
using StudentRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentRecords.Web.Orchestrator
{
    public class ApiOrchestrator : IApiOrchestrator
    {
        private readonly HttpClient _httpClient;

        public ApiOrchestrator()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44318/api/")
            };
        }

        public async Task<IEnumerable<Course>> GetCourse(string endpoint = "")
        {
            var result = await _httpClient.GetAsync($"Course/{endpoint}");
            if(result.IsSuccessStatusCode)
            {
                var course = await result.Content.ReadAsAsync<IEnumerable<Course>>();
                return course;
            }
            return null;
        }

        public async Task<IEnumerable<Student>> GetStudent(string endpoint = "")
        {
            var result = await _httpClient.GetAsync($"Students/{endpoint}");
            if (result.IsSuccessStatusCode)
            {
                var student = await result.Content.ReadAsAsync<IEnumerable<Student>>();
                return student;
            }
            return null;
        }

        public async Task<HttpResponseMessage> Post(object objectToPost, string endpoint = "")
        {
            var json = JsonConvert.SerializeObject(objectToPost);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(endpoint, stringContent);
            return result;
        }
    }
}
