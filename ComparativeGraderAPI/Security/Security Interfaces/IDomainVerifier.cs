using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Security.Security_Interfaces
{
    public interface IDomainVerifier
    {
        void Verify<T>(object ob);
    }
}
