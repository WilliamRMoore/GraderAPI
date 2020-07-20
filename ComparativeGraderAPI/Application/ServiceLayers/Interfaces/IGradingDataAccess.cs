using ComparativeGraderAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.ServiceLayers.Interfaces
{
    public interface IGradingDataAccess
    {
        Task<bool> AddSemester(Semester semester);
    }
}
