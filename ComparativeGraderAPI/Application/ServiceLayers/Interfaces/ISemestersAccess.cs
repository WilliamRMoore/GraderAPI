using ComparativeGraderAPI.Application.ServiceLayers.DataAccess;
using ComparativeGraderAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.ServiceLayers.Interfaces
{
    public interface ISemestersAccess
    {
        Task<bool> AddSemester(Semester semester);
        Task<List<Semester>> ListSemesters();
    }
}
