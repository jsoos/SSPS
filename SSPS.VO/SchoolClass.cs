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
            var ret = new List<SchoolClass>(17);
            ret.Add(new SchoolClass("ALL"));
            for (int i = 1; i < 5; i++)
            {
                ret.Add(new SchoolClass($"{i}.A"));
                ret.Add(new SchoolClass($"{i}.B"));
                ret.Add(new SchoolClass($"{i}.C"));
                ret.Add(new SchoolClass($"{i}.D"));
                ret.Add(new SchoolClass($"{i}.L"));
            }
            return ret;
        }
    }
}
