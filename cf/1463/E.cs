using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            int n = Read<int>(), k = Read<int>();
            List<int>[] G = new List<int>[n + 1];
            int[] pr = new int[n + 1], fa = new int[n + 1], deg = new int[n + 1], pa = new int[n + 1];
            for (int i = 0; i <= n; ++i) G[i] = new();
            for (int i = 1; i <= n; ++i) {
                int u = Read<int>();
                G[u].Add(i); ++deg[i];
                pa[i] = u;
                fa[i] = i;
            }
            for (int i = 0; i < k; ++i) {
                int x = Read<int>(), y = Read<int>();
                pr[x] = y; 
                if (!Merge(x, y)) {
                    output.Append(0);
                    goto end;
                }
                G[x].Add(y); ++deg[y];
            }
            int[] tot = new int[n + 1], sz = new int[n + 1];
            for (int i = 1; i <= n; ++i) {
                ++sz[Find(i)];
                if (Find(pa[i]) == fa[i]) --deg[i];
                if (deg[i] == 1 && fa[i] != i) {
                    ++tot[fa[i]];
                    --deg[i];
                }
            }
            Queue<int> q = new();
            HashSet<int> tmp = new();
            List<int> ans = new();
            q.Enqueue(0);
            while (q.Count > 0) {
                int u = q.Dequeue();
                do {
                    ans.Add(u);
                    foreach (var v in G[u]) {
                        if (v == pr[u] || deg[v] <= 0) continue;
                        --deg[v];
                        if (deg[v] == 1 && fa[v] != v) {
                            ++tot[fa[v]];
                            --deg[v];
                            tmp.Add(fa[v]);
                        }
                        else if (deg[v] == 0 && fa[v] == v) {
                            ++tot[v];
                            tmp.Add(v);
                        }
                    }
                    u = pr[u];
                    // --deg[u];
                } while (u != 0);
                foreach (var v in tmp) {
                    if (sz[v] == tot[v]) {
                        q.Enqueue(v);
                    }
                }
                tmp.Clear();
            }
            if (ans.Count > n) output.AppendJoin(' ', ans.Skip(1));
            else output.Append(0);
            end:  Console.WriteLine(output.ToString());

            int Find(int x) => fa[x] == x ? fa[x] : fa[x] = Find(fa[x]);
            bool Merge(int x, int y) {
                // note the order: x <- y
                x = Find(x);
                y = Find(y);
                if (x == y) return false;
                fa[y] = x;
                return true;
            }
        }
    }
}