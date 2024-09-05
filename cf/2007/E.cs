using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionE a = new();
a.Solve();
#endif

namespace Template
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
            while (T-- > 0)
            {
                int n = Read<int>();
                long w = Read<long>();
                int[] p = new int[n]; p[0] = -1;
                List<int>[] paths = new List<int>[n], cover = new List<int>[n];
                int u;
                for (int i = 0; i < n; ++i)
                {
                    paths[i] = new();
                    cover[i] = new();
                }
                for (int i = 1; i < n; ++i)
                {
                    p[i] = Read<int>() - 1;
                    u = i - 1;
                    while (u != p[i])
                    {
                        paths[i - 1].Add(u); // (i-1 -> i) contains edge (u - p[u])
                        cover[u].Add(i - 1);
                        u = p[u];
                    }
                    paths[i - 1].Add(i);
                    cover[i].Add(i - 1);
                }
                u = n - 1;
                while (u != 0)
                {
                    paths[^1].Add(u);
                    cover[u].Add(n - 1);
                    u = p[u];
                }
                long sum = n * w;
                int[] cnt = new int[n];
                long[] sump = new long[n];
                int de = 0;
                for (int i = 1; i < n; ++i)
                {
                    var j = Read<int>() - 1;
                    long t = Read<long>();
                    var pre = de;
                    w -= t;
                    foreach (var P in cover[j])
                    {
                        if (++cnt[P] == paths[P].Count)
                        {
                            sum -= w;
                            de++;
                        }
                    }
                    // w -= t;
                    sum -= (n - pre - 2) * t;
                    output.AppendFormat("{0} ", sum);
                }
                output.AppendLine();
            }
            Console.WriteLine(output.ToString());
        }
    }
}