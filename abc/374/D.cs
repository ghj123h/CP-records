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
            int n = Read<int>(), s = Read<int>(), t = Read<int>();
            int[][] co = new int[n][];
            for (int i = 0; i < n; ++i) {
                co[i] = new int[4];
                for (int j = 0; j < 4; ++j) co[i][j] = Read<int>();
            }
            int[] p = Enumerable.Range(0, n).ToArray();
            double tot = co.Sum(c => Dis(c[0], c[1], c[2], c[3]));
            double ans = double.MaxValue;
            do {
                for (int u = 0; u < (1 << n); ++u) {
                    double cur = 0;
                    int b = u & 1;
                    cur += Dis(0, 0, co[p[0]][b * 2], co[p[0]][b * 2 + 1]);
                    for (int i = 1; i < n; ++i) {
                        int j = (u >> i) & 1, k = (u >> (i - 1)) & 1;
                        cur += Dis(co[p[i - 1]][2 - k * 2], co[p[i - 1]][3 - k * 2], co[p[i]][j * 2], co[p[i]][1 + j * 2]);
                    }
                    ans = Math.Min(ans, cur);
                }
            } while (NextPermutation(p));
            output.AppendFormat("{0:F10}\n", ans / s + tot / t);
            Console.Write(output.ToString());
        }

        public static double Dis(int x0, int y0, int x1, int y1)
            => Math.Sqrt((x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1));

        public static bool NextPermutation<T>(IList<T> a) where T : IComparable {
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
            while (i < j) {
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