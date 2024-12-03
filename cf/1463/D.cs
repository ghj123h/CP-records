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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] b = ReadArray<int>(n);
                bool[] vis = new bool[2 * n + 1];
                int r = 1, i, j;
                foreach (var a in b) vis[a] = true;
                for (i = 0; i < n; ++i) {
                    r = Math.Max(r, b[i]);
                    while (r <= 2 * n && vis[r]) ++r;
                    if (r > 2 * n) break;
                    else vis[r] = true;
                }
                j = i; // x <= j
                r = 2 * n; Array.Fill(vis, false);
                foreach (var a in b) vis[a] = true;
                for (i = n - 1; i >= 0; --i) {
                    r = Math.Min(r, b[i]);
                    while (r >= 1 && vis[r]) --r;
                    if (r == 0) break;
                    else vis[r] = true;
                } // n - x <= n - i - 1 => x >= i + 1
                output.Append(j - i).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}