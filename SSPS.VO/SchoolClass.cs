using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Diagnostics;

namespace SSPS.VO
{
    [DataContract]
    [DebuggerDisplay("{Name}")]
    public class SchoolClass
    {
        [DataMember]
        public string Name { get; private set; }
        public int Year
        {
            get
            {
                if (Name != "ALL")
                    return string.IsNullOrEmpty(Name) ? 0 : int.Parse(Name[0].ToString());
                else
                    return -1;
            }
        }
        [DataMember]
        public List<Supplementation> Supplementations { get; set; }

        public SchoolClass() { }

        public SchoolClass(string Name)
        {
            this.Name = Name;
            Supplementations = new List<Supplementation>();
        }
        public SchoolClass(string Name, List<Supplementation> Supplementatation)
        {
            this.Name = Name;
            this.Supplementations = Supplementations;
        }
        /// <summary>
        /// Generate instances of all posible classes
        /// </summary>
        /// <returns>All posible classes</returns>
        public static List<SchoolClass> GetAllClasses()
        {
            var res = new List<SchoolClass>(17);
            //ret.Add(new SchoolClass("ALL"));
            for (int i = 1; i < 5; i++)
            {
                res.Add(new SchoolClass(string.Format("{0}.A", i)));
                res.Add(new SchoolClass(string.Format("{0}.B", i)));
                res.Add(new SchoolClass(string.Format("{0}.V", i)));
                res.Add(new SchoolClass(string.Format("{0}.F", i)));
                res.Add(new SchoolClass(string.Format("{0}.L", i)));
            }
            return res;
        }
    }
}
