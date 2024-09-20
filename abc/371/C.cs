using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateC;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace TemplateC
{
    internal class SolutionC
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
            bool[,] g = new bool[n, n], h = new bool[n, n];
            int[,] a = new int[n, n];
            int[] p = Enumerable.Range(0, n).ToArray();
            ReadGraph(g); ReadGraph(h);
            for (int i = 0; i < n; ++i) for (int j = i + 1; j < n; ++j) a[i, j] = a[j, i] = Read<int>();
            long ans = long.MaxValue;
            do
            {
                long res = 0;
                for (int u = 0; u < n; ++u) for (int v = u + 1; v < n; ++v)
                    {
                        if (((g[u, v] && !h[p[u], p[v]]) || (!g[u, v] && h[p[u], p[v]]))) res += a[p[u], p[v]];
                    }
                ans = Math.Min(ans, res);
            } while (NextPermutation(p));
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());

            void ReadGraph(bool[,] G)
            {
                int m = Read<int>();
                while (m-- > 0)
                {
                    int u = Read<int>(), v = Read<int>();
                    u--; v--;
                    G[u, v] = G[v, u] = true;
                }
            }
        }

        bool NextPermutation<T>(IList<T> a) where T : IComparable
        {
            if (a.Count < 2) return false;
            var k = a.Count - 2;

            while (k >= 0 && a[k].CompareTo(a[k + 1]) >= 0) k--;
            if (k < 0) return false;

            var l = a.Count - 1;
            while (l > k && a[l].CompareTo(a[k]) <= 0) l--;

            var tmp = a[k];
            a[k] = a[l];
            a[l] = tmp;

            var i = k + 1;
            var j = a.Count - 1;
            while (i < j)
            {
                tmp = a[i];
                a[i] = a[j];
                a[j] = tmp;
                i++;
                j--;
            }

            return true;
        }
    }
}