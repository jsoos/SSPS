using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSPS.VO
{
    public class Timetable
    {
        public SchoolClass SchoolClass { get; set; }
        public List<Day> Days { get; set; }
        public Timetable(string className)
        {
            SchoolClass = new SchoolClass(className);
            Days = new List<Day>();
        }
    }
}
