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
            int n = Read<int>(), mod = 998244353;
            int[] w = ReadArray<int>(n);
            long ans = 1;
            for (int i = 0; i < n; i += 3) {
                if (w[i] == w[i + 1] && w[i + 1] == w[i + 2]) {
                    ans = ans * 3 % mod;
                } else if (w[i] == w[i + 1] || w[i] == w[i + 2] || w[i + 1] == w[i + 2]) {
                    int u = w[i] ^ w[i + 1] ^ w[i + 2], v = (w[i] + w[i + 1] + w[i + 2] - u) / 2;
                    if (u > v) ans = ans * 2 % mod;
                }
            }
            long fact = 1;
            for (int i = 2; i <= n / 6; ++i) fact = fact * i % mod;
            long inv = Inv(fact, mod);
            for (int i = n / 6 + 1; i <= n / 3; ++i) fact = fact * i % mod;
            ans = ans * fact % mod * inv % mod * inv % mod;
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());
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