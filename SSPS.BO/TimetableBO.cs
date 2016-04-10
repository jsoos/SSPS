using SSPS.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSPS.BO
{
    public class TimetableBO
    {
        private static List<Timetable> timetable;

        public static Timetable GetTimetableForClass(string className)
        {
            if (timetable != null)
                return timetable.Find(x=>x.SchoolClass.Name == className);

            timetable = new List<Timetable>();

            XDocument doc = XDocument.Parse(getStreamFromResources("SSPS.BO.ClassesTimetable.xml"));
            List<Teacher> teachers = new List<Teacher>();
            List<Subject> subjects = new List<Subject>();
            var doc2 = doc.Element("school").Elements("subjects").Elements();
            foreach (var item in doc.Element("school").Elements("subjects").Elements())
            {
                Teacher teacher;
                if (teachers.Any(x => x.Shortcut == item.LastAttribute.Value))
                    teacher = teachers.Find(x => x.Shortcut == item.LastAttribute.Value);
                else
                {
                    teacher = new Teacher() { Shortcut = item.LastAttribute.Value };
                    teachers.Add(teacher);
                }
                var subject = new Subject()
                {
                    Id = int.Parse(item.Attribute("id").Value),
                    Name = item.Attribute("name").Value,
                    Teacher = teacher
                };
                subjects.Add(subject);
            }
            var a = doc.Element("school").Element("classes").Elements("class");
            foreach (var @class in doc.Element("school").Element("classes").Elements("class"))
            {
                var timetableNode = @class.Element("timetable");
                var timetable = new Timetable(@class.FirstAttribute.Value);
                foreach (var dayNode in timetableNode.Elements())
                {
                    Day day = new Day();
                    day.Hours = int.Parse(dayNode.LastAttribute.Value);
                    day.Type = dayNode.FirstAttribute.Value;

                    foreach (var subjectNode in dayNode.Elements())
                    {
                        DaySubject subject = new DaySubject(subjects.Find(x=>x.Id == int.Parse(subjectNode.FirstAttribute.Value)));
                        subject.Hour = int.Parse(subjectNode.LastAttribute.Value);
                        day.Subjects.Add(subject);
                    }
                timetable.Days.Add(day);
                }
                TimetableBO.timetable.Add(timetable);
            }

            return timetable.Find(x=>x.SchoolClass.Name == className);
        }

        private static string getStreamFromResources(string path)
        {
            var stream = typeof(TimetableBO).GetTypeInfo().Assembly.GetManifestResourceStream(path);
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
