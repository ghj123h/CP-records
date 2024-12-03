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

        public void Solve() {
            StringBuilder output = new();
            int n = Read<int>(), m = Read<int>();
            List<int>[] G = new List<int>[n + 1];
            (int u, int v)[] e = new (int u, int v)[m];
            int[] d = new int[n + 1];
            for (int i = 1; i <= n; ++i) G[i] = new();
            for (int i = 0; i < m; ++i) {
                int u = Read<int>(), v = Read<int>();
                G[u].Add(v);
                ++d[v];
                e[i] = (u, v);
            }
            Queue<int> q = new();
            for (int i = 1; i <= n; ++i) if (d[i] == 0) q.Enqueue(i);
            int tot = 0;
            while (q.Count > 0) {
                int u = q.Dequeue(); ++tot;
                foreach (var v in G[u]) {
                    if (--d[v] == 0) q.Enqueue(v);
                }
            }
            if (tot != n) output.AppendLine("2").AppendJoin(' ', e.Select(e => e.u > e.v ? 1 : 2));
            else output.AppendLine("1").AppendJoin(' ', Enumerable.Repeat(1, m));
            Console.WriteLine(output.ToString());
        }
    }
}