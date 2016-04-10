using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSPS.VO
{
    public class Day : IEnumerable<List<DaySubject>>
    {
        public string Name { get; private set; }

        public int Hours { get; set; }

        public string Type { get; set; }

        public List<DaySubject> Subjects { get; set; }

        public Day()
        {
            Subjects = new List<DaySubject>();
        }

        public IEnumerator<List<DaySubject>> GetEnumerator()
        {
            var grouped = Subjects.GroupBy(x => x.Hour);
            var list = grouped.Select(x => x.ToList()).ToList();
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
