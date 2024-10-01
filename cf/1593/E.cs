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
                int n = Read<int>(), k = Read<int>();
                List<int>[] G = new List<int>[n];
                for (int i = 0; i < n; ++i) G[i] = new();
                if (n == 1) { output.AppendLine("0"); continue;  }
                int[] deg = new int[n];
                for (int i = 1; i < n; ++i) {
                    int u = Read<int>() - 1, v = Read<int>() - 1;
                    G[u].Add(v); G[v].Add(u);
                    ++deg[u]; ++deg[v];
                }
                Queue<(int, int)> q = new();
                int ans = n;
                for (int i = 0; i < n; ++i) if (deg[i] == 1) q.Enqueue((i, 1));
                while (q.Count > 0) {
                    var (u, d) = q.Dequeue();
                    if (d > k) continue;
                    --ans;
                    foreach (var v in G[u]) if (--deg[v] == 1) q.Enqueue((v, d + 1));
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}