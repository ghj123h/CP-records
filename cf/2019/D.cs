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
                int[] a = ReadArray<int>(n);
                int[] l = new int[n + 1], r = new int[n + 1];
                Array.Fill(l, -1); Array.Fill(r, -1);
                for (int i = 0; i < n; ++i) {
                    if (l[a[i]] < 0) l[a[i]] = i;
                    r[a[i]] = i;
                }
                int ans = 0;
                bool suc = true;
                int L = n, R = -1, cnt = 0;
                int[] D = new int[n + 1];
                for (int i = 1; i <= n; ++i) {
                    if (l[i] >= 0) {
                        L = Math.Min(L, l[i]);
                        R = Math.Max(R, r[i]);
                        if (R - L >= i) { suc = false; break; }
                        else {
                            int d = i - (R - L + 1);
                            int ll = Math.Max(L - d, 0);
                            int rr = Math.Min(R + d, n - 1) + 1;
                            D[ll]++;
                            D[rr]--;
                            ++cnt;
                        }
                    }
                }
                if (!suc) output.Append(0).AppendLine();
                else {
                    for (int i = 1; i <= n; ++i) D[i] += D[i - 1];
                    output.Append(D.Count(x => x == cnt)).AppendLine();
                }
            }
            Console.Write(output.ToString());
        }
    }
}