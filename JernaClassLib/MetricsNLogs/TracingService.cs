using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaClassLib.MetricsNLogs
{
    public class TracingService
    {
        public static string ActivitySourceName = "Jerna-Activity-Source-Name";
        public static ActivitySource ActualActivitySource = new(ActivitySourceName);
    }
}
