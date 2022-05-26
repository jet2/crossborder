using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kppApp
{
    public static class TimeLord
    {
        public static long UTCNow()
        {
            return (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
        public static int timezone_seconds()
        {
            DateTime localtime = DateTime.Now;
            return (int)(localtime - localtime.ToUniversalTime()).TotalSeconds;
        }
    }
}
