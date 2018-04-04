using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashFreeTimetable
{
    class DetailedCourse
    {
        public Course course { get; set; }
        public int remainingC_Hours { get; set; }
        public bool monday { get; set; }
        public bool tuesday { get; set; }
        public bool wednesday { get; set; }
        public bool thursday { get; set; }
        public bool friday { get; set; }

        public DetailedCourse ()
        {
            monday = false;
            tuesday = false;
            wednesday = false;
            thursday = false;
            friday = false;
        }
    }
}
