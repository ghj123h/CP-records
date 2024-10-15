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
            int T = Read<int>();
            long[] fact = new long[maxn + 1], inv_fact = new long[maxn + 1];
            fact[0] = inv_fact[0] = 1;
            for (int i = 1; i <= maxn; ++i) {
                fact[i] = fact[i - 1] * i % Mod;
                inv_fact[i] = Inv(fact[i]);
            }
            while (T-- > 0)
            {
                int n = Read<int>(), l = Read<int>(), r = Read<int>();
                if (n % 2 == 0) output.Append(Cal(n / 2)).AppendLine();
                else output.Append((Cal(n / 2) + Cal(n / 2 + 1)) % Mod).AppendLine();

                long Cal(int u) { // positive = u
                    int k = Math.Min(1 - l, r - n);
                    long ans = k * C(n, u) % Mod;
                    for (++k; ; ++k) {
                        int a = Math.Max(0, n - r + k); // a must negative
                        int b = Math.Max(0, l + k - 1); // b must positive
                        if (a > n - u || b > u) break;
                        ans += C(n - a - b, u - b);
                        ans %= Mod;
                    }
                    return ans;
                }
            }

            long C(int n, int k) => fact[n] * inv_fact[k] % Mod * inv_fact[n - k] % Mod;
            Console.Write(output.ToString());
        }

        public static readonly int Mod = 1000000007;
        public static readonly int maxn = 200010;

        public static long Inv(long a) {
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