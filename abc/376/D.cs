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
            List<int>[] G = new List<int>[n + 1];
            for (int i = 1; i <= n; ++i) G[i] = new();
            for (int i = 0; i < m; ++i) {
                int u = Read<int>(), v = Read<int>();
                G[u].Add(v);
            }
            Queue<int> q = new();
            int[] d = new int[n + 1];
            int ans = -1;
            d[1] = 1;
            q.Enqueue(1);
            while (q.Count > 0) {
                int u = q.Dequeue();
                foreach (var v in G[u]) {
                    if (v == 1) {
                        ans = d[u];
                        goto https;
                    } else if (d[v] == 0) {
                        d[v] = d[u] + 1;
                        q.Enqueue(v);
                    }
                }
            }
        https://atcoder.jp/contests/abc376/tasks/abc376_d
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}