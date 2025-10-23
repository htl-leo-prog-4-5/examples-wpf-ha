using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcGGT
{
    public class GGT
    {
        public int Zahl1 { get; set; }
        public int Zahl2 { get; set; }

        public int Ggt
        {
            get
            {
                return CalcMyGgt(Zahl1, Zahl2);
            }
        }

        static int CalcMyGgt(int n, int m)
        {
            return n == m ? n : n < m ? CalcMyGgt(n, m - n) : CalcMyGgt(n - m, m);
        }
    }
}
