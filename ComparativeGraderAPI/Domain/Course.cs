using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Domain
{
    public class Course
    {
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public string ProfessorUserId { get; set; }
        public int Year { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
