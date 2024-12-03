using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB;

#if !PROBLEM
SolutionB a = new();
a.Solve();
#endif

namespace TemplateB
{
    internal class SolutionB
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
                long[] a = ReadArray<long>(n);
                long ans;
                if (n == 1) {
                    ans = 1;
                } else if (n % 2 == 0) {
                    ans = 0;
                    for (int i = 0; i < n; i += 2) ans = Math.Max(ans, a[i + 1] - a[i]);
                } else {
                    long[] pre = new long[n], suf = new long[n];
                    for (int i = 0; i < n - 1; i += 2) pre[i] = a[i + 1] - a[i];
                    for (int i = n - 1; i > 0; i -= 2) suf[i] = a[i] - a[i - 1];
                    for (int i = 2; i < n; i += 2) pre[i] = Math.Max(pre[i], pre[i - 2]);
                    for (int i = n - 3; i >= 0; i -= 2) suf[i] = Math.Max(suf[i], suf[i + 2]);
                    ans = Math.Min(pre[n - 3], suf[2]);
                    for (int i = 2; i < n - 2; i += 2) ans = Math.Min(ans, Math.Max(pre[i - 2], suf[i + 2]));
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}