using HtmlAgilityPack;
using SSPS.VO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSPS.BO
{
    internal class SupplementationNode
    {
        public HtmlNode DateNode
        {
            set
            {
                var dateStrings = value.ChildNodes[1].InnerText.Replace("nbsp;", "").Trim().Split('-', '&').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                DateFrom = DateTime.Parse(dateStrings[1]);
                if (dateStrings.Length > 2)
                    DateTo = DateTime.Parse(dateStrings[3]);
                else
                    DateTo = DateFrom;

                var update = value.ChildNodes[3].InnerText.Replace("&nbsp;", " ").Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                Updated = DateTime.Parse(update);
            }
        }
        public List<HtmlNode> SupplementationNodes { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime Updated { get; set; }

        public SupplementationNode(HtmlNode DateNode)
        {
            this.DateNode = DateNode;
            this.SupplementationNodes = new List<HtmlNode>();
        }

        public List<SchoolClass> GetSupplementationForDay()
        {
            var schoolClasses = SchoolClass.GetAllClasses();

            Updated.IsDaylightSavingTime();

            foreach (var item in this.SupplementationNodes)
            {
                if (item.Attributes["CLASS"].Value.Contains("b"))
                {
                    foreach (var @class in schoolClasses)
                        @class.Supplementations.Add(new Supplementation() { Message = item.InnerText.Trim(), From = DateFrom, To = DateTo, Updated = Updated });

                    continue;
                }
                var nodeWithChanges = item.ChildNodes["UL"];
                foreach (var change in nodeWithChanges.ChildNodes.Where(x => !string.IsNullOrEmpty(x.InnerText.Trim())))
                {
                    foreach (var @class in schoolClasses.Where(x => change.InnerText.Contains(x.Name) || change.InnerText.Contains($"{x.Year}.r")))
                    {
                        @class.Supplementations.Add(new Supplementation() { Message = change.InnerText.Trim(), From = DateFrom, To = DateTo, Updated = Updated });
                    }
                }
            }

            return schoolClasses;
        }
    }
}