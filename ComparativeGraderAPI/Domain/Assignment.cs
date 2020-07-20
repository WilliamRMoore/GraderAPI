using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Domain
{
    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        //public string ProfessorUserId { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public DateTime DueDate { get; set; }
        public ICollection<Submission>  Submissions { get; set; }
    }
}
