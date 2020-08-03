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
            //var auth = _gradingDataContext.Assignments.Find(submission.AssignmentId).ProfessorUserId.Equals(_userAccessor.GetCurrentUserId());
            //var auth = _gradingDataContext.Assignments.Any(a => a.Id == submission.Id && a.ProfessorUserId == _userAccessor.GetCurrentUserId());

            //if (!auth)
            //{
            //    throw new RestException(HttpStatusCode.Unauthorized, new { activity = "UNAUTHORIZED" });
            //}

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

        public async Task<bool> DeleteSubmission(int id)
        {
            var submission = await _gradingDataContext.Submissions.FindAsync(id);

            _gradingDataContext.Submissions.Remove(submission);

            var success = await _gradingDataContext.SaveChangesAsync() > 0;

            if (success)
            {
                return success;
            }

            throw new Exception("Problem saving changes.");
        }

        public async Task<bool> EditSubmission(Command submissionEdits)
        {
            var currentSubmission = await _gradingDataContext.Submissions.FindAsync(submissionEdits.SubmissionId);

            currentSubmission.StudentName = submissionEdits.StudentName ?? currentSubmission.StudentName;
            //currentSubmission.Rank = submissionEdits.Rank;
            currentSubmission.Grade = submissionEdits.Grade;

            var success = await _gradingDataContext.SaveChangesAsync() > 0;

            if (success)
            {
                return success;
            }

            throw new Exception("Problem saving changes.");
        }
    }
}
