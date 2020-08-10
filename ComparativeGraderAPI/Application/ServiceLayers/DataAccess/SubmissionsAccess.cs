using ComparativeGraderAPI.Application.Errors;
using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Persistence;
using ComparativeGraderAPI.Security.Security_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//using static ComparativeGraderAPI.Application.Submissions.Create;
using static ComparativeGraderAPI.Application.Submissions.Edit;

namespace ComparativeGraderAPI.Application.ServiceLayers.DataAccess
{
    public class SubmissionsAccess : ISubmissionsAccess
    {
        private readonly IUserAccessor _userAccessor;
        private readonly GradingDataContext _gradingDataContext;

        public SubmissionsAccess(IUserAccessor userAccessor, GradingDataContext gradingDataContext)
        {
            _userAccessor = userAccessor;
            _gradingDataContext = gradingDataContext;
        }

        public async Task<Submission> AddSubmission(Submission submission)
        {

            submission.ProfessorUserId = _userAccessor.GetCurrentUserId();

            _gradingDataContext.Submissions.Add(submission);

            var success = await _gradingDataContext.SaveChangesAsync() > 0;

            if (success) return submission;

            throw new Exception("Problem saving changes");
        }

        public async Task<IEnumerable<Submission>> ListSubmissions()
        {
            var submissions = _gradingDataContext.Submissions.Where(s => s.ProfessorUserId == _userAccessor.GetCurrentUserId());

            return submissions;
        }

        public async Task<Submission> SubmissionDetails(int id)
        {
            var submission = await _gradingDataContext.Submissions.FindAsync(id);

            return submission;
        }

        public async Task<bool> DeleteSubmission(Submission submission)
        {
            _gradingDataContext.Submissions.Remove(submission);

            var success = await _gradingDataContext.SaveChangesAsync() > 0;

            if (success)
            {
                return success;
            }

            throw new Exception("Problem saving changes.");
        }

        public async Task<bool> EditSubmission(Command submissionEdits, Submission submissionToEdit)
        {
            //var currentSubmission = await _gradingDataContext.Submissions.FindAsync(submissionEdits.SubmissionId);

            submissionToEdit.StudentName = submissionEdits.StudentName ?? submissionToEdit.StudentName;
            //currentSubmission.Rank = submissionEdits.Rank;
            submissionToEdit.Grade = submissionEdits.Grade;

            var success = await _gradingDataContext.SaveChangesAsync() > 0;

            if (success)
            {
                return success;
            }

            throw new Exception("Problem saving changes.");
        }
    }
}
