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
            int T = Read<int>(), mod = 998244353;
            long[] fact = new long[200010], inv_fact = new long[200010];
            fact[0] = inv_fact[0] = 1;
            for (int i = 1; i < fact.Length; ++i) {
                fact[i] = fact[i - 1] * i % mod;
                inv_fact[i] = Inv(fact[i], mod);
            }
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                int[] f = new int[n];
                foreach (var v in a) f[v]++;
                int[] min = new int[n];
                min[0] = f[0];
                for (int i = 1; i < n; ++i) min[i] = Math.Min(f[i], min[i - 1]);
                long[][] dp = new long[n][];
                long[] suf = new long[n + 1], sufc = new long[n + 1];
                dp[0] = new long[f[0] + 1];
                for (int j = 1; j <= f[0]; ++j) dp[0][j] = C(f[0], j);
                
                for (int i = 1; i < n; ++i) {
                    sufc[f[i]] = 1;
                    suf[f[i - 1]] = dp[i - 1][f[i - 1]];
                    dp[i] = new long[f[i] + 1];
                    for (int j = f[i - 1] - 1; j >= 0; --j) suf[j] = (suf[j + 1] + dp[i - 1][j]) % mod;
                    for (int j = f[i] - 1; j >= 0; --j) sufc[j] = (sufc[j + 1] + C(f[i], j)) % mod;
                    for (int j = 1; j <= min[i]; ++j) {
                        if (j < f[i - 1]) dp[i][j] = suf[j + 1] * C(f[i], j) % mod;
                        dp[i][  j] += sufc[j] * dp[i - 1][j] % mod;
                        dp[i][j] %= mod;
                    }
                }
                long p2 = 1, ans = 0;
                for (int i = n - 1; i >= 0; --i) {
                    for (int j = 1; j <= min[i]; ++j) {
                        ans = (ans + dp[i][j] * j % mod * p2) % mod;
                    }
                    p2 = p2 * qpow(2, f[i]) % mod;
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());

            long C(long n, long k) {
                if (k < 0 || k > n) return 0;
                return fact[n] * inv_fact[k] % mod * inv_fact[n - k] % mod;
            }

            long qpow(long n, long e) {
                long res = 1;
                while (e > 0) {
                    if ((e & 1) > 0) res = res * n % mod;
                    n = n * n % mod;
                    e >>= 1;
                }
                return res;
            }
        }

        public static long Inv(long a, long Mod) {
            long u = 0, v = 1, m = Mod;
            while (a > 0) {
                long t = m / a;
                m -= t * a; (a, m) = (m, a);
                u -= t * v; (u, v) = (v, u);
            }
            return (u % Mod + Mod) % Mod;
        }
    }
}