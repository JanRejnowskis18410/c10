using Cw10.DTOs.Requests;
using Cw10.Exceptions;
using Cw10.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Services
{
    public class EfDbService : IDbService
    {
        public s18410Context _context { get; set; }
        public EfDbService(s18410Context context)
        {
            _context = context;
        }

        public List<Student> GetStudents()
        {
            return _context.Student.ToList();
        }

        public void UpdateStudent(string index, UpdateStudentRequest request)
        {
            try
            {
                var st = new Student();
                st.IndexNumber = index;
                _context.Attach(st);
                var firstName = request.FirstName;
                var lastName = request.LastName;
                var birthDate = request.BirthDate;
                var idEnrollment = request.IdEnrollment;
                var entry = _context.Entry(st);
                if (firstName != null)
                {
                    st.FirstName = firstName;
                    entry.Property("FirstName").IsModified = true;
                }
                if (lastName != null)
                {
                    st.LastName = lastName;
                    entry.Property("LastName").IsModified = true;
                }
                if (birthDate == null)
                {
                    entry.Property("BirthDate").IsModified = true;
                }
                if (idEnrollment > 0)
                {
                    st.IdEnrollment = idEnrollment;
                    entry.Property("IdEnrollment").IsModified = true;
                }
                _context.SaveChanges();
            }catch (DbUpdateException exc)
            {
                throw new DBException(exc.InnerException.Message);
            }
        }
    }
}
