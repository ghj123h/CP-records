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
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>(), h = Read<int>();
                List<(int, int)>[] G = new List<(int, int)>[2 * n + 1];
                for (int i = 1; i <= 2 * n; ++i) G[i] = new();
                for (int i = 0; i < h; ++i) {
                    int u = Read<int>();
                    G[u].Add((u + n, 0));
                }
                for (int i = 0; i < m; ++i) {
                    int u = Read<int>(), v = Read<int>(), w = Read<int>();
                    G[u].Add((v, w)); G[v].Add((u, w));
                    G[u + n].Add((v + n, w / 2)); G[v + n].Add((u + n, w / 2));
                }
                var p = Dijkstra(1);
                var q = Dijkstra(n);
                long ans = 0x3f3f3f3f3f3f3f3f;
                for (int i = 1; i <= n; ++i) {
                    ans = Math.Min(ans,
                        Math.Min(
                            Math.Min(Math.Max(p[i], q[i]), Math.Max(p[i + n], q[i])),
                            Math.Min(Math.Max(p[i], q[i + n]), Math.Max(p[i + n], q[i + n]))
                            ));
                }
                output.Append(ans == 0x3f3f3f3f3f3f3f3f ? -1 : ans).AppendLine();

                long[] Dijkstra(int s) {
                    long[] d = new long[2 * n + 1];
                    bool[] vis = new bool[2 * n + 1];
                    Array.Fill(d, 0x3f3f3f3f3f3f3f3f);
                    PriorityQueue<int, long> q = new();
                    q.Enqueue(s, d[s] = 0);
                    while (q.Count > 0) {
                        int u = q.Dequeue();
                        if (vis[u]) continue;
                        vis[u] = true;
                        foreach (var (v, w) in G[u]) {
                            if (d[v] > d[u] + w) {
                                d[v] = d[u] + w;
                                q.Enqueue(v, d[v]);
                            }
                        }
                    }
                    return d;
                }
            }
            Console.Write(output.ToString());
        }
    }
}