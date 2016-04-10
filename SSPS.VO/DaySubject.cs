using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SSPS.VO
{
    [DebuggerDisplay("{Hour} => {Name}")]
    public class DaySubject : Subject
    {
        public int Hour { get; set; }
        
        public DaySubject(Subject subject)
        {
            this.Id = subject.Id;
            this.Name = subject.Name;
            this.Teacher = subject.Teacher;
            this.Changed = subject.Changed;
        }
    }
}
