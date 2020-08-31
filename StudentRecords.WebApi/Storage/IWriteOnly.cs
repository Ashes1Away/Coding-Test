using StudentRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecords.WebApi.Storage
{
    public interface IWriteOnly
    {
        void Upsert(Student student);
        void AddEnrolmentInfo(Enrolment enrolment);
    }
}
