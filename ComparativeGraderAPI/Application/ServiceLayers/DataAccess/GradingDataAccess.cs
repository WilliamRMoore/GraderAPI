using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.ServiceLayers.DataAccess
{
    public class GradingDataAccess : IGradingDataAccess
    {
        private readonly GradingDataContext _context;

        public GradingDataAccess(GradingDataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddSemester(Semester semester)
        {
            _context.Semesters.Add(semester);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return success;

            throw new Exception("Problem saving changes");
        }
    }
}
