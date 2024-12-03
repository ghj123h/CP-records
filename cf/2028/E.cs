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
            int T = Read<int>(), mod = 998244353;
            while (T-- > 0)
            {
                int n = Read<int>();
                List<int>[] G = new List<int>[n];
                for (int i = 0; i < n; ++i) G[i] = new();
                for (int i = 1; i < n; ++i) {
                    int u = Read<int>() - 1, v = Read<int>() - 1;
                    G[u].Add(v);
                    G[v].Add(u);
                }
                int[] fa = new int[n], dep = new int[n], sz = new int[n];
                long[] ans = new long[n];
                ans[0] = 1;
                dfs(0);
                
                void dfs(int u) {
                    sz[u] = 1;
                    foreach (var v in G[u]) {
                        if (v == fa[u]) continue;
                        fa[v] = u;
                        dep[v] = dep[u] + 1;
                        dfs(v);
                        sz[u] += sz[v];
                    }
                }

                foreach (var (_, u) in dep.Select((d, i) => (d, i)).OrderBy(p => p)) {
                    if (sz[u] > 1) continue;
                    int v, cnt = 0;
                    for (v = u; ans[v] == 0; v = fa[v]) ++cnt;
                    long f = ans[v] * Inv(cnt, mod) % mod;
                    for (v = u; ans[fa[v]] == 0; v = fa[v]) {
                        ans[fa[v]] = (ans[v] + f) % mod;
                    }
                }
                output.AppendJoin(' ', ans).AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static long Inv(long a, long Mod) {
            long u = 0, v = 1, m = Mod;
            while (a > 0) {
                long t = m / a;
                m -= t * a; (a, m) = (m, a);
                u -= t * v; (u, v) = (v, u);
            }
            return (u % Mod + Mod) % Mod;
        }
    }
}