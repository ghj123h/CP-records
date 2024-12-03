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
            int n = Read<int>(), m = Read<int>();
            int[] x = ReadArray<int>(m);
            int[] term = new int[m - 1], p = Enumerable.Range(0, n + 1).ToArray();
            long[] ans = new long[n];
            List<int>[] ef = new List<int>[n + 1];
            for (int i = 1; i <= n; ++i) ef[i] = new();
            for (int i = 0; i < m - 1; ++i) {
                ans[0] += term[i] = Math.Abs(x[i + 1] - x[i]);
                ef[x[i + 1]].Add(i);
                ef[x[i]].Add(i);
            }
            // pos(p, i): [1,2,3,4] -> [2,1,3,4] -> [2,3,1,4] -> [2,3,4,1]
            for (int i = 1; i < n; ++i) {
                ans[i] = ans[i - 1];
                (p[i], p[i + 1]) = (p[i + 1], p[i]);
                for (int j = i; j < i + 2; ++j) {
                    foreach (var k in ef[j]) {
                        int t = term[k];
                        term[k] = Math.Abs(p[x[k + 1]] - p[x[k]]);
                        ans[i] += term[k] - t;
                    }
                }
            }
            output.AppendJoin(' ', ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}