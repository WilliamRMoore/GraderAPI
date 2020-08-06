﻿using ComparativeGraderAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Security.Security_Interfaces
{
    public interface ICourseVerifier
    {
        void Verify(Course course);
    }
}
