using Cw10.DTOs.Requests;
using Cw10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Services
{
    public interface IDbService
    {
        List<Student> GetStudents();
        void UpdateStudent(string index, UpdateStudentRequest request);
    }
}
