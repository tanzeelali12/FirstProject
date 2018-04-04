using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashFreeTimetable
{
    class CellCourse
    {
        public int ID { get; set; }
        public String code { get; set; }//
        public String originalName { get; set; }//
        public String name { get; set; }//
        public String section { get; set; }//
        public int c_hour { get; set; }//
        public String venue { get; set; }//
        public String timeFrom { get; set; }//
        public String timeTo { get; set; }//
        public String day { get; set; }//
        public int semester { get; set; }//
    }
}
