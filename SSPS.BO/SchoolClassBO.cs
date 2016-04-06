using System;
using System.Collections.Generic;
using System.Linq;
using SSPS.VO;
using System.Threading.Tasks;
using Portable.Text;

namespace SSPS.BO
{
    public class SchoolClassBO
    {
        /// <summary>
        /// Async method which get all posible data for <see cref="SchoolClass"/>
        /// </summary>
        /// <returns>Return listed <see cref="SchoolClass"/></returns>
        public async static Task<List<SchoolClass>> List()
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            var doc = await web.LoadFromWebAsync("http://www.ssps.cz/pages/rozvrhy/", Encoding.GetEncoding("windows-1250"));
            // Get into neareast node to all data
            var mainData = doc.DocumentNode.ChildNodes["html"].ChildNodes["body"].ChildNodes["table"]
                .GetNode("tr",5).GetNode("td", 2).GetNode("div", 1)
                .GetNode("table",1).GetNode("tr", 7).ChildNodes["td"].ChildNodes.Where(x => x.InnerText.Trim() != string.Empty);

            var parsedData = new List<SupplementationNode>();
            SupplementationNode active = null;
            foreach (var item in mainData)
            {
                if (item.ChildNodes.Skip(1).First().Attributes.Any(x => x.Name.ToLower() == "class") && item.ChildNodes.Skip(1).First().Attributes["CLASS"].Value == "fll")
                {
                    if (active != null)
                        parsedData.Add(active);
                    active = new SupplementationNode(item);
                    continue;
                }
                active.SupplementationNodes.Add(item);
            }
            if (active != null)
                parsedData.Add(active);
            var result = new List<SchoolClass>();
            foreach (var item in parsedData)
            {
                result.AddRange(item.GetSupplementationForDay());
            }

            var resultGrouped = SchoolClass.GetAllClasses();
            foreach (var x in result)
            {
                resultGrouped.SingleOrDefault(y => y.Name == x.Name).Supplementations.AddRange(x.Supplementations);
            }
            
            return resultGrouped.ToList();
        }
        /// <summary>
        /// Async method which get all posible data for selected date
        /// </summary>
        /// <param name="date">Date which is wanted</param>
        /// <returns>Return listed <see cref="SchoolClass"/> filtered by date</returns>
        public static async Task<List<SchoolClass>> ListDate(DateTime date)
        {
            var all = await List();
            var res = SchoolClass.GetAllClasses();
            foreach (var item in all)
            {
                foreach (var supplementation in item.Supplementations)
                {
                    if (date >= supplementation.From && supplementation.To <= date)
                        res.Single(x => x.Name == item.Name).Supplementations.Add(supplementation);
                }
            }
            return res;
        }
        /// <summary>
        /// Get data for selected class 
        /// </summary>
        /// <param name="className">Name of class which is wanted</param>
        /// <returns>Return listed <see cref="SchoolClass"/> filtered by class</returns>
        public static async Task<SchoolClass> GetClass(string className)
        {
            return (await List()).SingleOrDefault(x => x.Name == className);
        }
    }
}
static class Extensions
{
    public static HtmlAgilityPack.HtmlNode GetNode(this HtmlAgilityPack.HtmlNode node, string name, int count)
    {
        return node.ChildNodes.Where(x => x.Name == name).ToArray()[count-1];
    }
}