using ComparativeGraderAPI.Application.ServiceLayers.Interfaces;
using ComparativeGraderAPI.Domain;
using ComparativeGraderAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.ServiceLayers.DataAccess
{
    public class SemestersAccess : ISemestersAccess
    {
        private readonly GradingDataContext _context;

        public SemestersAccess(GradingDataContext context)
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
        public async Task<List<Semester>> ListSemesters()
        {
            return await _context.Semesters.ToListAsync();
        }
    }
}
