using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE2;

#if !PROBLEM
SolutionE2 a = new();
a.Solve();
#endif

namespace TemplateE2
{
    internal class SolutionE2
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
            long[] fact = new long[200010], inv_fact = new long[200010];
            int mod = 1000000007;
            fact[0] = inv_fact[0] = 1;
            for (int i = 1; i < fact.Length; ++i) {
                fact[i] = fact[i - 1] * i % mod;
                inv_fact[i] = Inv(fact[i], mod);
            }
            while (T-- > 0) {
                int n = Read<int>(), m = Read<int>(), k = Read<int>();
                int[] a = ReadArray<int>(n);
                int[] mp = new int[n + 1];
                foreach (var x in a) ++mp[x];
                int[] pre = new int[n + k + 2];
                for (int i = 1; i <= n + k; ++i) pre[i + 1] = pre[i] + (i <= n ? mp[i] : 0);
                long ans = 0;
                for (int i = 1; i <= n; ++i) {
                    int v = pre[i + k + 1] - pre[i], u = v - mp[i];
                    ans += (C(v, m) - C(u, m) + mod) % mod;
                    ans %= mod;
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());

            long C(int n, int k) {
                if (k >= 0 && k <= n)
                    return fact[n] * inv_fact[k] % mod * inv_fact[n - k] % mod;
                else return 0;
            }
        }

        public static long Inv(long a, long mod) {
            long u = 0, v = 1, m = mod;
            while (a != 0) {
                long t = m / a;
                m -= t * a; (a, m) = (m, a);
                u -= t * v; (u, v) = (v, u);
            }
            return (u % mod + mod) % mod;
        }
    }
}