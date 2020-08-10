using ComparativeGraderAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ComparativeGraderAPI.Application.Submissions.Edit;

namespace ComparativeGraderAPI.Application.ServiceLayers.Interfaces
{
    public interface ISubmissionsAccess
    {
        Task<Submission> AddSubmission(Submission submission);
        Task<IEnumerable<Submission>> ListSubmissions();
        Task<Submission> SubmissionDetails(int id);
        Task<bool> DeleteSubmission(Submission submission);
        Task<bool> EditSubmission(Command submissionEdits, Submission submissionToEdit);
    }
}
