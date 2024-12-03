using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
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
                long[] a = ReadArray<long>(n);
                SortedDictionary<long, List<long>> G = new();
                for (int i = 1; i < n; ++i) {
                    if (G.TryGetValue(a[i] + i, out var lst)) lst.Add(i);
                    else G.Add(a[i] + i, new List<long> { i });
                }
                SortedDictionary<long, long> dp = new();
                output.Append(dfs(n)).AppendLine();

                long dfs(long u) {
                    if (dp.TryGetValue(u, out long r)) return r;
                    r = u;
                    if (G.TryGetValue(u, out var lst)) {
                        foreach (var v in lst) {
                            r = Math.Max(r, dfs(u + v));
                        }
                    }
                    dp.Add(u, r);
                    return r;
                }
            }
            Console.Write(output.ToString());
        }
    }
}