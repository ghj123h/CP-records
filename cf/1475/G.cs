using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateG;

#if !PROBLEM
SolutionG a = new();
a.Solve();
#endif

namespace TemplateG
{
    internal class SolutionG
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
                SortedDictionary<int, int> mp = new();
                int mx = 0;
                for (int i = 0; i < n; ++i) {
                    int u = Read<int>();
                    mx = Math.Max(mx, u);
                    if (mp.TryGetValue(u, out int v)) mp[u] = v + 1;
                    else mp.Add(u, 1);
                }
                int[] d = new int[mx + 1];
                int ans = 0;
                foreach (var (v, f) in mp) {
                    ans = Math.Max(ans, d[v] += f);
                    for (int i = 2 * v; i <= mx; i += v) d[i] = Math.Max(d[i], d[v]);
                }
                output.Append(n - ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}