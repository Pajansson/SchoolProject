using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_assignment_1.Services
{
    public class FakeTimeProvider : ITimeProvider
    {
        private DateTime CurrentTime { get; set; }

        public FakeTimeProvider()
        {
            ResetTime();
        }

        public void ResetTime()
        {
            CurrentTime = DateTime.Now;
        }

        public void TimeWarp()
        {
            CurrentTime = CurrentTime.Date.AddMonths(3);
        }
    }
}
