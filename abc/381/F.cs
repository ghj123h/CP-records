using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TemplateF;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace TemplateF
{
    internal class SolutionF
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
            int n = Read<int>();
            int[] a = ReadArray<int>(n);
            int[,] nxt = new int[20, n + 1];
            for (int j = 0; j < 20; ++j) {
                int r = n;
                nxt[j, n] = r;
                for (int i = n - 1; i >= 0; --i) {
                    nxt[j, i] = r;
                    if (a[i] == j + 1) r = i;
                }
            }
            int[] dp = new int[1 << 20];
            int ans = 0;
            for (int i = 1; i < (1 << 20); ++i) {
                dp[i] = n;
                for (int j = 0; j < 20; ++j) {
                    if ((i >> j & 1) > 0) {
                        int v;
                        if (i == (1 << j) && a[0] == j + 1) v = nxt[j, 0];
                        else v = nxt[j, nxt[j, dp[i ^ (1 << j)]]];
                        dp[i] = Math.Min(dp[i], v);
                    }
                }
                if (dp[i] < n) ans = Math.Max(ans, BitOperations.PopCount((uint)i));
            }
            Console.WriteLine(ans * 2);
        }
    }
}