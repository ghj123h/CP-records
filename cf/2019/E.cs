using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
            while (T-- > 0)
            {
                int n = Read<int>();
                List<int>[] G = new List<int>[n + 1];
                for (int i = 1; i <= n; ++i) G[i] = new();
                for (int i = 1; i < n; ++i) {
                    int u = Read<int>(), v = Read<int>();
                    G[u].Add(v); G[v].Add(u);
                }
                int[] d = new int[n + 1], maxd = new int[n + 1], f = new int[n + 1], son = new int[n + 1], top = new int[n + 1];
                dfs(1, 0);
                dfs2(1, 1);
                Queue<int> q = new();
                int toDel = 0, cur = 1, sz = 0, ans = 0;
                q.Enqueue(1);
                while (q.Count > 0) {
                    var u = q.Dequeue();
                    if (d[u] > cur) {
                        ans = Math.Max(sz, ans);
                        cur = d[u];
                        sz -= toDel;
                        toDel = 0;
                    }
                    ++sz;
                    foreach (var v in G[u]) if (v != f[u]) q.Enqueue(v);
                    if (son[u] == 0) toDel += d[u] - d[top[u]] + 1;
                }
                ans = Math.Max(sz, ans);
                output.Append(n - ans).AppendLine();

                void dfs(int u, int fa) {
                    f[u] = fa;
                    d[u] = d[fa] + 1;
                    foreach (var v in G[u]) {
                        if (v != fa) {
                            dfs(v, u);
                            if (maxd[u] < maxd[v] + 1) {
                                maxd[u] = maxd[v] + 1;
                                son[u] = v;
                            }
                        }
                    }
                }

                void dfs2(int u, int rt) {
                    top[u] = rt;
                    foreach (var v in G[u]) {
                        if (v != f[u]) {
                            if (v == son[u]) dfs2(v, rt);
                            else dfs2(v, v);
                        }
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}