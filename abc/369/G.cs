using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionG a = new();
a.Solve();
#endif

namespace Template
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
            int n = Read<int>();
            List<(int, int)>[] G = new List<(int, int)>[n + 1];
            for (int i = 1; i <= n; ++i) G[i] = new();
            for (int i = 1; i < n; ++i)
            {
                int u = Read<int>(), v = Read<int>(), w = Read<int>();
                G[u].Add((v, w)); G[v].Add((u, w));
            }
            int[] fa = new int[n + 1], son = new int[n + 1];
            long[] dep = new long[n + 1], len = new long[n];
            int cnt = 0;
            dfs1(1);
            dfs2(1, 0);
            Array.Sort(len, (a, b) => b.CompareTo(a));
            for (int i = 1; i < n; ++i) len[i] += len[i - 1];
            output.AppendJoin('\n', len.Select(l => l * 2));
            Console.Write(output.ToString());

            void dfs1(int u)
            {
                son[u] = -1;
                foreach (var (v, w) in G[u]) if (v != fa[u])
                    {
                        fa[v] = u;
                        dfs1(v);
                        if (dep[u] < dep[v] + w)
                        {
                            dep[u] = dep[v] + w;
                            son[u] = v;
                        }
                    }
            }
            void dfs2(int u, int id)
            {
                foreach (var (v, w) in G[u]) if (v != fa[u])
                    {
                        if (v == son[u])
                        {
                            len[id] += w;
                            dfs2(v, id);
                        }
                        else
                        {
                            len[++cnt] = w;
                            dfs2(v, cnt);
                        }
                    }
            }
        }
    }
}