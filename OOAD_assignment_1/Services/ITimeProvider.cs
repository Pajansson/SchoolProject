using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_assignment_1.Services
{
    public interface ITimeProvider
    {
        void TimeWarp();
        void ResetTime();
        DateTime GetTime();
    }
}
