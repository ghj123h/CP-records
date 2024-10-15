using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            int n = Read<int>(), k = Read<int>();
            int[,] next = new int[k, n + 2];
            int[] dp = new int[1 << k];
            sr.ReadLine();
            string s = sr.ReadLine();
            int L = 1, R = n / k + 1;
            while (L < R) {
                int mid = L + (R - L) / 2;
                next.AsSpan().Fill(n);
                for (int j = 0; j < k; ++j) {
                    char c = (char)(j + 'a');
                    int l = 0, cnt = 0;
                    for (int r = 0; r < n; ++r) {
                        if (s[r] == c || s[r] == '?') {
                            if (++cnt == mid) {
                                while (l <= r - cnt + 1) next[j, l++] = r;
                                --cnt;
                            }
                        } else {
                            cnt = 0;
                        }
                    }
                }
                Array.Fill(dp, n);
                dp[0] = -1;
                for (int u = 1; u < (1 << k); ++u) {
                    int v = u;
                    while (v > 0) {
                        int j = BitOperations.TrailingZeroCount(v);
                        dp[u] = Math.Min(dp[u], next[j, dp[u ^ (1 << j)] + 1]);
                        v ^= 1 << j;
                    }
                }
                if (dp[^1] < n) L = mid + 1;
                else R = mid;
            }
            Console.WriteLine(L - 1);
        }
    }

    public static class ArrayExt {
        public static Span<T> AsSpan<T>(this T[,] array) => asSpan<T>(array);
        public static Span<T> AsSpan<T>(this T[,,] array) => asSpan<T>(array);
        static Span<T> asSpan<T>(Array array)
            => System.Runtime.InteropServices.MemoryMarshal.CreateSpan(
                ref System.Runtime.CompilerServices.Unsafe.As<byte, T>(
                    ref System.Runtime.InteropServices.MemoryMarshal.GetArrayDataReference(array)
                ), array.Length);
    }
}