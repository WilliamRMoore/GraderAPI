using ComparativeGraderAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ComparativeGraderAPI.Application.Assingments.Edit;

namespace ComparativeGraderAPI.Application.ServiceLayers.Interfaces
{
    public interface IAssignmentAccess
    {
        Task AddAssignment(Assignment assignment);
        Task<IEnumerable<Assignment>> ListAssignments();
        Task<Assignment> AssignmentDetails(int id);
        Task EditAssignment(Command assignmentEdits, Assignment assignment);
        Task DeleteAssignment(int id);
    }
}
