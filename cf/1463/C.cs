using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using TemplateC;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace TemplateC
{
    internal class SolutionC
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
                int n = Read<int>();
                long[] t = new long[n + 1];
                long[] x = new long[n + 1];
                for (int i = 0; i < n; ++i) {
                    t[i] = Read<int>();
                    x[i] = Read<int>();
                }
                t[n] = 3L * int.MaxValue;
                long nt = 0, nx = 0, ans = 0, cur = 0;
                bool mv = true;
                for (int i = 0; i < n; ++i) {
                    if (nt <= t[i]) {
                        nt = t[i] + Math.Abs(x[i] - nx);
                        mv = x[i] >= nx;
                        nx = x[i];
                    }
                    long nxt = cur + (mv ? 1 : -1) * (t[i + 1] - t[i]);
                    if (mv) nxt = Math.Min(nx, nxt);
                    else nxt = Math.Max(nx, nxt);
                    if (mv ? nxt >= x[i] && cur <= x[i] : nxt <= x[i] && cur >= x[i]) ++ans;
                    cur = nxt;
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}