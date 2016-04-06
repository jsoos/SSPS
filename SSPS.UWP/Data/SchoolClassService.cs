using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPS.VO;
using SSPS.BO;
using System.Diagnostics;

namespace SSPS.UWP.Data
{
    class SchoolClassService
    {
        public static string Name = "SchollClass Data Service";
        private static List<SchoolClass> data;
        private static DateTime lastUpdate;
        private static TimeSpan refreshTimeSpan = TimeSpan.FromMinutes(1);
        public async static Task<List<SchoolClass>> List()
        {
            Debug.Write("List SchollClasses - ");
            if (lastUpdate.Add(refreshTimeSpan) >= DateTime.Now)
            {
                Debug.WriteLine("Loaded from cache");
                return data;
            }
            Debug.WriteLine("Downloaded from web");
            data = await SchoolClassBO.List();
            lastUpdate = DateTime.Now;
            return data;
        }
    }
}
