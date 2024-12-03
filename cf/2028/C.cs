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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>(), v = Read<int>();
                long[] a = ReadArray<long>(n);
                int[] pre = new int[m + 1], suf = new int[m + 1];
                long[] sum = new long[n + 1];
                long c = 0;
                Array.Fill(pre, n + 1); pre[0] = 0;
                for (int i = 0, j = 1; i < n && j <= m; ++i) {
                    c += a[i];
                    if (c >= v) {
                        pre[j++] = i + 1;
                        c = 0;
                    }
                }
                Array.Fill(suf, -1); suf[0] = n; c = 0;
                for (int i = n - 1, j = 1; i >= 0 && j <= m; --i) {
                    c += a[i];
                    if (c >= v) {
                        suf[j++] = i;
                        c = 0;
                    }
                }
                long ans = -1;
                for (int i = 0; i < n; ++i) sum[i + 1] = sum[i] + a[i];
                for (int i = 0; i <= m; ++i) {
                    if (pre[i] <= suf[m - i]) {
                        ans = Math.Max(ans, sum[suf[m - i]] - sum[pre[i]]);
                    }
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}