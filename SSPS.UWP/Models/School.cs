using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSPS.UWP.Models
{
    class School
    {
        public string Name { get; set; }
        public List<VO.SchoolClass> Classes { get; set; }

        public School(string name)
        {
            this.Name = name;
            this.Classes = Data.SchoolClassService.List().Result;
        }
        
    }
}
