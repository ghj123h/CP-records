using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE;

#if !PROBLEM
SolutionE a = new();
a.Solve();
#endif

namespace TemplateE
{
    internal class SolutionE
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
            int[,] d = new int[5, 5] {
                {0,-1,-1,-1,-1 },
                {-1,0,0,1,0 },
                {-1,2,0,1,0 },
                {-1,1,1,0,1 },
                {-1,2,2,1,0 }
                };
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                int max = 0;
                for (int i = 0; i < n; ++i) {
                    if (a[i] % 2 == 1) a[i] += a[i] % 10;
                    max = Math.Max(max, a[i]);
                }
                var (u, v) = (max / 10, max % 10 / 2);
                bool suc = true;
                for (int i = 0; i < n && suc; ++i) {
                    var (x, y) = (a[i] / 10, a[i] % 10 / 2);
                    if (d[y, v] >= 0 && d[y, v] % 2 == (u - x) % 2) {
                        if (v == 0 && u != x) suc = false;
                    } else suc = false;
                }
                output.AppendLine(suc ? "Yes" : "No");
            }
            Console.Write(output.ToString());
        }
    }
}