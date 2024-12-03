using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB;

#if !PROBLEM
SolutionB a = new();
a.Solve();
#endif

namespace TemplateB
{
    internal class SolutionB
    {
        private readonly StreamReader sr = new(Console.OpenStandardInput());
        private T Read<T>()
            where T : struct, IConvertible
        {
            char c;
            dynamic res = default(T);
            dynamic sign = 1;
            while (!sr.EndOfStream && char.IsWhiteSpace((char)sr.Peek())) sr.Read();
            if (!sr.EndOfStream && (char)sr.Peek() == '-')
            {
                sr.Read();
                sign = -1;
            }
            while (!sr.EndOfStream && char.IsDigit((char)sr.Peek()))
            {
                c = (char)sr.Read();
                res = res * 10 + c - '0';
            }
            return res * sign;
        }

        private T[] ReadArray<T>(int n)
            where T : struct, IConvertible
        {
            T[] arr = new T[n];
            for (int i = 0; i < n; ++i) arr[i] = Read<T>();
            return arr;
        }

        public void Solve()
        {
            StringBuilder output = new();
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>(), x = Read<int>();
                int d, h;
                int maxd = 0, maxm = 0;
                for (int i = 0; i < n; ++i) {
                    d = Read<int>(); h = Read<int>();
                    maxd = Math.Max(maxd, d);
                    maxm = Math.Max(maxm, d - h);
                }
                if (x <= maxd) {
                    output.AppendLine("1");
                } else if (maxm == 0) {
                    output.AppendLine("-1");
                } else {
                    output.Append((x - maxd + maxm - 1) / maxm + 1).AppendLine();
                }
            }
            Console.Write(output.ToString());
        }
    }
}