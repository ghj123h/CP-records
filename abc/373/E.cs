using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE;

#if !PROBLEM
SolutionE a = new();
a.Solve();
#endif

namespace TemplateE
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
            long k = Read<long>();
            long[] a = ReadArray<long>(n);
            int[] idx = Enumerable.Range(0, n).ToArray();
            long[] ans = new long[n];
            if (n > m) {
                Array.Sort(a, idx);
                long[] pre = new long[n + 1];
                for (int i = 0; i < n; ++i) pre[i + 1] = pre[i] + a[i];
                long RR = k - a.Sum();
                for (int i = 0; i < n; ++i) {
                    long L = 0, R = RR + 1;
                    while (L < R) {
                        long mid = L + (R - L) / 2;
                        var j = UpperBound(a, a[i] + mid);
                        int p = n - j; int q = m - p;
                        long sum;
                        if (n - m > i) sum = pre[j] - pre[n - m];
                        else sum = pre[j] - pre[n - m - 1] - a[i];
                        if (q > 0 && sum + RR - mid < (a[i] + mid + 1) * q) R = mid;
                        else L = mid + 1;
                    }
                    if (L == RR + 1) ans[idx[i]] = -1;
                    else ans[idx[i]] = L;
                }
            }
            output.AppendJoin(' ', ans).AppendLine();
            Console.Write(output.ToString());
        }

        public static int UpperBound(IList<long> A, long u) {
            int L = 0, R = A.Count;
            while (L < R) {
                int m = L + (R - L) / 2;
                if (A[m] <= u) L = m + 1;
                else R = m;
            }
            return L;
        }
    }
}