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

        public void Solve()
        {
            StringBuilder output = new();
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>();
                long c = Read<long>();
                long[] a = ReadArray<long>(n);
                List<int>[] G = new List<int>[n + 1];
                for (int i = 1; i <= n; ++i) G[i] = new();
                for (int i = 1; i < n; ++i) {
                    int u = Read<int>(), v = Read<int>();
                    G[u].Add(v); G[v].Add(u);
                }
                long[,] d = new long [n + 1, 2]; // 2nd: is root good;
                dfs(1, 0);
                output.Append(Math.Max(d[1, 0], d[1, 1])).AppendLine();

                void dfs(int u, int fa) {
                    foreach (var v in G[u]) if (v != fa) dfs(v, u);
                    d[u, 1] = a[u - 1];
                    
                    foreach (var v in G[u]) {
                        d[u, 1] += Math.Max(d[v, 1] - 2 * c, d[v, 0]);
                        d[u, 0] += Math.Max(d[v, 1], d[v, 0]);
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}