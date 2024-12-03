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
            int T = 1;
            while (T-- > 0)
            {
                int n = Read<int>();
                long[] a = ReadArray<long>(n);
                long[] pre = new long[n + 1], bp = new long[n + 1], b = new long[n + 1];
                long[] row = new long[n + 1];
                for (int i = 0; i < n; ++i) {
                    pre[i + 1] = pre[i] + a[i];
                    bp[i + 1] = bp[i] + a[i] * (i + 1); // bp[i] = a1+2a2+...+iai
                }
                b[n] = a[n - 1];
                for (int i = n - 1; i >= 1; --i) {
                    b[i] = b[i + 1] + a[i - 1] * (n - i + 1); // b[n]=s(n,n), b[n-1]=s(n-1,n)+s(n-1,n-1), etc.
                }
                long[] p = new long[n + 1];
                row[1] = 1;
                for (int i = 1; i <= n; ++i) {
                    p[i] = p[i - 1] + b[i]; // p[i] = b[1] +.. + b[i]
                    if (i > 1) row[i] = row[i - 1] + n - i + 2;
                }
                int q = Read<int>();
                while (q-- > 0) {
                    long l = Read<long>(), r = Read<long>();
                    int rl = Array.BinarySearch(row, l), rr = Array.BinarySearch(row, r);
                    if (rl < 0) rl = ~rl - 1;
                    if (rr < 0) rr = ~rr - 1;
                    long ans = 0;
                    if (rl < rr) ans += p[rr - 1] - p[rl];
                    long cl = l - row[rl] + rl, cr = r - row[rr] + rr;
                    if (rl == rr) {
                        ans += Cal(rl, cr) - Cal(rl, cl - 1);
                    } else {
                        ans += Cal(rl, n) - Cal(rl, cl - 1) + Cal(rr, cr);
                    }
                    output.Append(ans).AppendLine();
                }

                // s(l, l) + ... s(l, r) = (r-l+1)*a_l + .. + a_r = (r+1)(+)-(la_l+..r_a_r)
                long Cal(long l, long r) {
                    if (r < l) return 0;
                    return (r + 1) * (pre[r] - pre[l - 1]) - (bp[r] - bp[l - 1]);
                }
            }
            Console.Write(output.ToString());
        }
    }
}