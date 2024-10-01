using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateF2;

#if !PROBLEM
SolutionF2 a = new();
a.Solve();
#endif

namespace TemplateF2
{
    internal class SolutionF2
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
            T[] arr = new T[n + 1];
            for (int i = 1; i <= n; ++i) arr[i] = Read<T>();
            return arr;
        }

        public void Solve()
        {
            StringBuilder output = new();
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                SortedDictionary<int, int> dis = new();
                int p = -1, m = 0;
                foreach (var x in a.Skip(1).OrderBy(x => x)) {
                    if (p != x) dis.Add(p = x, ++m);
                }
                int[] l = new int[m + 1], pos = new int[m + 1], cnt = new int[m + 1], r = new int[m + 1];
                Array.Fill(l, -1);
                for (int i = 1; i <= n; ++i) {
                    a[i] = dis[a[i]];
                    if (l[a[i]] < 0) l[a[i]] = pos[a[i]] = i;
                    r[a[i]] = i;
                    cnt[a[i]]++;
                }
                int ans = 1;
                int[,] d = new int[n + 1, 3];
                for (int i = 1; i <= n; ++i) {
                    d[i, 0] = d[pos[a[i]], 0] + 1;
                    d[i, 1] = Math.Max(d[pos[a[i]], 1], Math.Max(d[pos[a[i] - 1], 0], d[pos[a[i] - 1], 2])) + 1;
                    if (i == r[a[i]]) d[i, 2] = d[l[a[i]], 1] + cnt[a[i]] - 1;
                    pos[a[i]] = i;
                    for (int j = 0; j < 3; ++j) ans = Math.Max(d[i, j], ans);
                }
                output.Append(n - ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}