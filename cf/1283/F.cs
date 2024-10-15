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
            int n = Read<int>();
            int[] a = ReadArray<int>(n - 1);
            int[] deg = new int[n + 1];
            foreach (var v in a) deg[v]++;
            PriorityQueue<int, int> q = new();
            int[] max = Enumerable.Range(0, n + 1).ToArray();
            for (int i = 1; i <= n; ++i) if (deg[i] == 0) q.Enqueue(i, max[i]);
            int[] fa = new int[n + 1];
            bool suc = true;
            for (int i = n - 2; i >= 0; --i) {
                if (q.Count == 0) {
                    suc = false;
                    break;
                }
                var v = q.Dequeue();
                fa[v] = a[i];
                max[a[i]] = Math.Max(max[a[i]], v);
                if (--deg[a[i]] == 0) q.Enqueue(a[i], max[a[i]]);
            }
            if (suc) {
                List<(int, int)> e = new();
                int rt = 0;
                for (int i = 1; i <= n; ++i) if (fa[i] > 0) e.Add((i, fa[i])); else rt = i;
                output.Append(rt).AppendLine();
                foreach (var (u, v) in e) output.AppendFormat("{0} {1}\n", u, v);
            }
            Console.Write(output.ToString());
        }
    }
}