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
                int n = Read<int>();
                long[] a = ReadArray<long>(n), b = ReadArray<long>(n);
                long[] pre = new long[n + 1];
                for (int i = 0; i < n; ++i) pre[i + 1] = pre[i] + b[i];
                long[] d = new long[n];
                long[] ans = new long[n];
                for (int i = 0; i < n; ++i) {
                    int L = i, R = n + 1;
                    while (L < R) {
                        int mid = L + (R - L) / 2;
                        if (pre[mid] - pre[i] < a[i]) L = mid + 1;
                        else R = mid;
                    }
                    ++d[i];
                    if (R <= n) {
                        --d[R - 1];
                        ans[R - 1] += a[i] - (pre[R - 1] - pre[i]);
                    }
                }
                for (int i = 0; i < n; ++i) {
                    if (i > 0) d[i] += d[i - 1];
                    ans[i] += d[i] * b[i];
                }
                output.AppendJoin(' ', ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}