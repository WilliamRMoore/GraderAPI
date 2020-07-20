using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Domain
{
    public class Semester
    {
        public int Id { get; set; }
        //public string ProfessorUserId { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public string Season { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
