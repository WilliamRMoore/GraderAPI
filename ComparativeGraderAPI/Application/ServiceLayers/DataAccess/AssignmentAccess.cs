﻿using ComparativeGraderAPI.Application.Errors;
using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Persistence;
using ComparativeGraderAPI.Security.Security_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static ComparativeGraderAPI.Application.Assingments.Edit;

namespace ComparativeGraderAPI.Application.ServiceLayers.DataAccess
{
    public class AssignmentAccess : IAssignmentAccess
    {
        private readonly IUserAccessor _userAccessor;
        private readonly GradingDataContext _gradingDataContext;

        public AssignmentAccess(IUserAccessor userAccessor, GradingDataContext gradingDataContext)
        {
            _userAccessor = userAccessor;
            _gradingDataContext = gradingDataContext;
        }

        public async Task AddAssignment(Assignment assignment)
        {
            assignment.ProfessorUserId = _userAccessor.GetCurrentUserId();

            _gradingDataContext.Assignments.Add(assignment);

            var success = await _gradingDataContext.SaveChangesAsync() > 0;

            if (success) return;

            throw new Exception("Problem saving changes");
        }

        public async Task<IEnumerable<Assignment>> ListAssignments()
        {
            var assignments = _gradingDataContext.Assignments.Where(a => a.ProfessorUserId == _userAccessor.GetCurrentUserId());

            return assignments;
        }

        public async Task<Assignment> AssignmentDetails(int id)
        {
            var assignment = await _gradingDataContext.Assignments.FindAsync(id);

            return assignment;
        }

        public async Task EditAssignment(Command assignmentEdits, Assignment currentAssignment)
        {

            currentAssignment.AssignmentDescription = assignmentEdits.AssignmentDescription ?? currentAssignment.AssignmentDescription;
            currentAssignment.AssignmentName = assignmentEdits.AssignmentName ?? currentAssignment.AssignmentName;
            currentAssignment.DueDate = assignmentEdits.DueDate ?? currentAssignment.DueDate;

            var success = await _gradingDataContext.SaveChangesAsync() > 0;

            if(success)
            {
                return;
            }

            throw new Exception("Problem saving changes.");
        }

        public async Task DeleteAssignment(int id)
        {
            var assignment = await _gradingDataContext.Assignments.FindAsync(id);

            _gradingDataContext.Assignments.Remove(assignment);

            var success = await _gradingDataContext.SaveChangesAsync() > 0;

            if (success)
            {
                return;
            }

            throw new Exception("Problem saving changes.");
        }
    }
}
