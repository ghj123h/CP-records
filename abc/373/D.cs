using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateD;

#if !PROBLEM
SolutionD a = new();
a.Solve();
#endif

namespace TemplateD
{
    internal class SolutionD
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
            int n = Read<int>(), m = Read<int>();
            List<(int, int)>[] G = new List<(int, int)>[n + 1];
            for (int i = 1; i <= n; ++i) G[i] = new();
            long[] ans = new long[n + 1];
            bool[] vis = new bool[n + 1];
            for (int i = 0;i < m; ++i) {
                int u = Read<int>(), v = Read<int>(), w = Read<int>();
                G[u].Add((v, w)); G[v].Add((u, -w));
            }
            for (int i = 1; i <= n; ++i) if (!vis[i]) { vis[i] = true; dfs(i); }
            output.AppendJoin(' ', ans.Skip(1)).AppendLine();
            
            void dfs(int u) {
                foreach (var (v, w) in G[u]) if (!vis[v]) {
                    ans[v] = ans[u] + w;
                    vis[v] = true;
                    dfs(v);
                }
            }
            Console.Write(output.ToString());
        }
    }
}