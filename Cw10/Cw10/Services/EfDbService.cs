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
                if (birthDate != null)
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

        public void DeleteStudent(string index)
        {
            try
            {
                var student = new Student
                {
                    IndexNumber = index
                };
                _context.Attach(student);
                _context.Remove(student);
                _context.SaveChanges();
            }catch(DbUpdateException exc)
            {
                throw new DBException(exc.InnerException.Message);
            }
        }

        public void EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                if (_context.Student.Count(student => student.IndexNumber.Equals(request.IndexNumber)) > 0)
                    throw new StudentAlreadyExistsException("Student o podanym indeksie już istnieje w bazie danych!");
                var studies = _context.Studies.Where(study => study.Name.Equals(request.Studies));
                if (studies.Any())
                {
                    var idStudies = studies.Where(study => study.Name == request.Studies).Select(study => new { study.IdStudy }).First().IdStudy;
                    var idEnrollment = _context.Enrollment.Where(enrollment => enrollment.IdStudy == idStudies && enrollment.Semester == 1)
                                                          .OrderByDescending(enrollment => enrollment.StartDate)
                                                          .Select(enrollment => enrollment.IdEnrollment)
                                                          .First();
                }
                else
                {
                    throw new StudiesDoesNotExistException("Studia o podanej nazwie nie istnieją w bazie danych!");
                }
                //if(_context.Enrollment.Count(enrollment => enrollment.IdStudy.Equals(idStudies) && ) > 0)
                //{

                //}
            }
            catch (DbUpdateException exc)
            {
                throw new DBException(exc.InnerException.Message);
            }
        }
    }
}
