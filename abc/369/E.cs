using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionE a = new();
a.Solve();
#endif

namespace Template
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
            int n = Read<int>(), m = Read<int>();
            long[,] G = new long[n + 1, n + 1];
            G.AsSpan().Fill(0x3f3f3f3f3f3f3f3f);
            for (int i = 1; i <= n; ++i) G[i, i] = 0;
            int[][] e = new int[m + 1][];
            for (int i = 1; i <= m; ++i)
            {
                int u = Read<int>(), v = Read<int>(), w = Read<int>();
                e[i] = new int[] { u, v, w };
                G[u, v] = G[v, u] = Math.Min(G[u, v], w);
            }
            for (int k = 1; k <= n; ++k) for (int i = 1; i <= n; ++i) for (int j = 1; j <= n; ++j) G[i, j] = Math.Min(G[i, j], G[i, k] + G[k, j]);
            int q = Read<int>();
            while (q-- > 0)
            {
                int k = Read<int>();
                int[] b = ReadArray<int>(k);
                int[] p = Enumerable.Range(0, k).ToArray();
                long ans = long.MaxValue;
                do
                {
                    for (int mask = 0; mask < 32; ++mask)
                    {
                        long res = 0;
                        res += G[1, e[b[p[0]]][get(mask, 0)]] + e[b[p[0]]][2];
                        for (int i = 1; i < k; ++i)
                        {
                            res += G[e[b[p[i - 1]]][get(mask, i - 1) ^ 1], e[b[p[i]]][get(mask, i)]] + e[b[p[i]]][2];
                        }
                        res += G[e[b[p[^1]]][get(mask, k - 1) ^ 1], n];
                        ans = Math.Min(ans, res);
                    }
                } while (NextPermutation(p));
                output.Append(ans).AppendLine();
            }
            Console.WriteLine(output.ToString());
        }

        int get(int mask, int i) => (mask & (1 << i)) > 0 ? 1 : 0;

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