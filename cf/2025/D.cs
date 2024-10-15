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
            int[,] d = new int[m + 1, m + 1]; // d[i][j] = first i gain increase j it
            int[,] st = new int[m, m + 1], it = new int[m, m + 1];
            for (int i = 0, j = -1; i < n; ++i) {
                int a = Read<int>();
                if (a == 0) {
                    ++j;
                } else if (j >= 0) {
                    if (a > 0) st[j, a]++;
                    else it[j, -a]++;
                }
            }
            for (int i = 0; i < m; ++i) {
                for (int j = 1; j <= m; ++j) {
                    st[i, j] += st[i, j - 1];
                    it[i, j] += it[i, j - 1];
                }
            }
            for (int i = 0; i < m; ++i) {
                for (int j = 0; j <= i; ++j) {
                    // increase it
                    d[i + 1, j + 1] = Math.Max(d[i + 1, j + 1], d[i, j] + st[i, i - j] + it[i, j + 1]);
                    // increase st
                    d[i + 1, j] = Math.Max(d[i + 1, j], d[i, j] + st[i, i - j + 1] + it[i, j]);
                }
            }
            int ans = 0;
            for (int j = 0; j <= m; ++j) ans = Math.Max(ans, d[m, j]);
            output.Append(ans);
            Console.WriteLine(output.ToString());
        }
    }
}