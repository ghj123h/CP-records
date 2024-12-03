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
                List<(int, int)>[] G = new List<(int, int)>[n];
                for (int i = 0; i < n; ++i) G[i] = new();
                for (int i = 1; i < n; ++i) {
                    int u = Read<int>() - 1, v = Read<int>() - 1;
                    G[u].Add((v, i)); G[v].Add((u, i));
                }
                if (n % 3 != 0) {
                    output.AppendLine("-1");
                    continue;
                }
                int[] sz = new int[n];
                List<int> ans = new();
                bool suc = true;
                dfs(0, -1, -1);
                if (!suc) output.AppendLine("-1");
                else {
                    output.Append(ans.Count).AppendLine();
                    output.AppendJoin(' ', ans).AppendLine();
                }

                void dfs(int u, int fa, int e) {
                    sz[u] = 1;
                    foreach (var (v, ev) in G[u]) {
                        if (v != fa) {
                            dfs(v, u, ev);
                            sz[u] += sz[v];
                        }
                    }
                    if (sz[u] > 3) suc = false;
                    else if (sz[u] == 3) {
                        sz[u] = 0;
                        if (e > 0) ans.Add(e);
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}