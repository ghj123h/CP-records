using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateD1;

#if !PROBLEM
SolutionD1 a = new();
a.Solve();
#endif

namespace TemplateD1
{
    internal class SolutionD1
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
            long inf = 0x3f3f3f3f3f3f3f3f;
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>();
                int[] a = ReadArray<int>(n), b = ReadArray<int>(m);
                long[] pre = new long[n + 1];
                for (int i = 0; i < n; ++i) pre[i + 1] = pre[i] + a[i];
                long[,] dp = new long[n + 1, m];
                for (int i = 1; i <= n; ++i) {
                    for (int j = m - 1; j >= 0; --j) {
                        long v = pre[i] - b[j];
                        int k = Array.BinarySearch(pre, v);
                        if (k < 0) k = ~k;
                        if (k == i) dp[i, j] = inf;
                        else dp[i, j] = dp[k, j] + m - j - 1;
                    }
                    for (int j = 1; j < m; ++j) dp[i, j] = Math.Min(dp[i, j], dp[i, j - 1]);
                }
                output.Append(dp[n, m - 1] >= inf ? -1 : dp[n, m - 1]).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}