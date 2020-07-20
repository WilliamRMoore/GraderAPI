using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Domain
{
    public class Submission
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }

        //public int SubmittedArtifactId { get; set; }

        public string StudentName { get; set; }
        public int Rank { get; set; }
        public double Grade { get; set; }
    }
}
