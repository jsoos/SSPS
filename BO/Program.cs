using SSPS.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<string>> lines = new List<List<string>>();
            foreach (var day in TimetableBO.GetTimetableForClass("2.A").Days)
            {
                List<string> currentDay = new List<string>();
                foreach (var hour in day)
                {
                    List<string> currentHour = new List<string>();
                    foreach (var subject in hour)
                    {
                        currentHour.Add(subject.Name);
                    }
                    currentDay.Add(currentHour.Aggregate((i, j) => i + "/" + j));
                }
                lines.Add(currentDay);
            }
            int cells = lines.Max(y => y.Count());

            lines.ForEach(x =>
            {
                if (cells - x.Count > 1)
                {
                    var o = cells - x.Count;
                    for (int i = 0; i < o; i++)
                    {
                        x.Add("");
                    }
                }
            });
            var temp = lines;
            lines = new List<List<string>>();
            var temp2 = new List<string>();
            for (int i = 1; i <= cells; i++)
            {
                temp2.Add(string.Format("{0}.", i));
            }
            lines.Add(temp2);
            lines.AddRange(temp);
            Console.WriteLine(ConsoleUtility.PadElementsInLines(lines.Select(x=>x.ToArray()).ToList(), 3));
            Console.ReadLine();
        }
    }
}
public static class ConsoleUtility
{
    /// <summary>
    /// Converts a List of string arrays to a string where each element in each line is correctly padded.
    /// Make sure that each array contains the same amount of elements!
    /// - Example without:
    /// Title Name Street
    /// Mr. Roman Sesamstreet
    /// Mrs. Claudia Abbey Road
    /// - Example with:
    /// Title   Name      Street
    /// Mr.     Roman     Sesamstreet
    /// Mrs.    Claudia   Abbey Road
    /// <param name="lines">List lines, where each line is an array of elements for that line.</param>
    /// <param name="padding">Additional padding between each element (default = 1)</param>
    /// </summary>
    public static string PadElementsInLines(List<string[]> lines, int padding = 1)
    {
        // Calculate maximum numbers for each element accross all lines
        var numElements = lines[0].Length;
        var maxValues = new int[numElements];
        for (int i = 0; i < numElements; i++)
        {
            maxValues[i] = lines.Max(x => x[i].Length) + padding;
        }
        var sb = new StringBuilder();
        // Build the output
        bool isFirst = true;
        foreach (var line in lines)
        {
            if (!isFirst)
            {
                sb.AppendLine();
            }
            isFirst = false;
            for (int i = 0; i < line.Length; i++)
            {
                var value = line[i];
                // Append the value with padding of the maximum length of any value for this element
                sb.Append(value.PadRight(maxValues[i]));
            }
        }
        return sb.ToString();
    }
}