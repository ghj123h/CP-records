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
                int n = Read<int>(), m = Read<int>();
                int[,] a = new int[n, m], b = new int[n, m];
                for (int i = 0; i < n; ++i) for (int j = 0; j < m; ++j) a[i, j] = Read<int>();
                for (int i = 0; i < n; ++i) for (int j = 0; j < m; ++j) b[i, j] = Read<int>();
                Random r = new((int)DateTime.Now.Ticks);
                ulong[] hash = new ulong[n * m + 1];
                for (int i = 1; i <= n * m; ++i) hash[i] = (ulong)r.NextInt64();
                HashSet<ulong> ra = new(), ca = new(), rb = new(), cb = new();
                GetRC(a, ra, ca);
                GetRC(b, rb, cb);
                ra.UnionWith(rb); ca.UnionWith(cb);
                output.AppendLine(ra.Count == n && ca.Count == m ? "Yes" : "No");

                void GetRC(int[,] mat, HashSet<ulong> r, HashSet<ulong> c) {
                    for (int i = 0; i < n; ++i) {
                        ulong h = 0;
                        for (int j = 0; j < m; ++j) h ^= hash[mat[i, j]];
                        r.Add(h);
                    }
                    for (int j = 0; j < m; ++j) {
                        ulong h = 0;
                        for (int i = 0; i < n; ++i) h ^= hash[mat[i, j]];
                        c.Add(h);
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}