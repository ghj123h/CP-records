using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateF;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace TemplateF
{
    internal class SolutionF
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

        public void Solve() {
            StringBuilder output = new();
            int n = Read<int>();
            int[] a = ReadArray<int>(n);
            for (int i = 0; i < n; ++i) if (a[i] == 0) a[i] = -1;
            List<int>[] G = new List<int>[n];
            for (int i = 0; i < n; ++i) G[i] = new();
            for (int i = 1; i < n; ++i) {
                int u = Read<int>() - 1, v = Read<int>() - 1;
                G[u].Add(v); G[v].Add(u);
            }
            int[] down = new int[n], ans = new int[n];
            dfs(0, -1);
            ans[0] = down[0];
            dfs2(0, -1);
            output.AppendJoin(' ', ans).AppendLine();

            void dfs(int u, int fa) {
                down[u] = a[u];
                foreach (var v in G[u]) {
                    if (v == fa) continue;
                    dfs(v, u);
                    if (down[v] > 0) down[u] += down[v];
                }
            }

            void dfs2(int u, int fa) {
                foreach (var v in G[u]) {
                    if (v == fa) continue;
                    ans[v] = down[v];
                    if (ans[u] > 0) {
                        if (down[v] > 0) ans[v] = Math.Max(ans[u], ans[v]);
                        else ans[v] += ans[u];
                    }
                    dfs2(v, u);
                }
            }
            Console.Write(output.ToString());
        }
    }
}