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
            while (T-- > 0) {
                int n = Read<int>();
                List<int>[] G = new List<int>[n];
                int[] a = new int[n], b = new int[n];
                for (int i = 0; i < n; ++i) G[i] = new();
                for (int i = 1; i < n; ++i) {
                    int p = Read<int>() - 1;
                    G[i].Add(p); G[p].Add(i);
                    a[i] = Read<int>();
                    b[i] = Read<int>();
                }
                List<long> cur = new() { 0 };
                long[] ans = new long[n];
                long[] d = new long[n];
                dfs(0, -1, 1, 0);
                output.AppendJoin(' ', ans.Skip(1)).AppendLine();

                void dfs(int u, int fa, int i, long sum) {
                    int l = i, r = cur.Count;
                    while (l < r) {
                        int m = l + (r - l) / 2;
                        if (cur[m] <= d[u]) l = m + 1;
                        else r = m;
                    }
                    ans[u] = l - 1;
                    foreach (var v in G[u]) {
                        if (v != fa) {
                            cur.Add(cur[^1] + b[v]);
                            d[v] = d[u] + a[v];
                            dfs(v, u, l, sum);
                            cur.RemoveAt(cur.Count - 1);
                        }
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}