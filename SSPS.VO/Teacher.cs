using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSPS.VO
{
    public class Teacher
    {
        public string Name { get; set; }
        public string Shortcut { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}
