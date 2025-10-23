using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoQuickTip
{
    class LottoTip
    {
        private const int TIPSIZE = 6;

        public UInt16[] QuickTip(UInt16 max)
        {
            if (max < TIPSIZE)
                throw new ArgumentException();

            var tips = new List<UInt16>();
            var rnd = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < TIPSIZE; i++)
            {
                UInt16 nexttip = (UInt16)rnd.Next(1, max + 1);
                while (tips.Contains(nexttip))
                {
                    nexttip = (UInt16)rnd.Next(1, max + 1);
                }
                tips.Add(nexttip);
            }
            return tips.ToArray();
        }

        public void WriteToFile(string filename, UInt16[] tips)
        {
            System.IO.File.WriteAllText(Environment.ExpandEnvironmentVariables(filename), $"{string.Join(";",tips)}\n");
        }
        public void AppendToFile(string filename, UInt16[] tips)
        {
            System.IO.File.AppendAllText(Environment.ExpandEnvironmentVariables(filename), $"{string.Join(";", tips)}\n");
        }
    }
}
