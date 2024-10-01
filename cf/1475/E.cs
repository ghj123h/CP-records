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
            int T = Read<int>();
            int mod = 1000000007;
            long[] fact = new long[1024], inv_fact = new long[1024];
            fact[0] = inv_fact[0] = 1;
            for (int i = 1; i <= 1000; ++i) {
                fact[i] = fact[i - 1] * i % mod;
                inv_fact[i] = Inv(fact[i], mod);
            }
            while (T-- > 0)
            {
                int n = Read<int>(), k = Read<int>();
                int[] a = ReadArray<int>(n);
                Array.Sort(a, (x, y) => y.CompareTo(x));
                int l = k - 1, r = k - 1;
                while (l >= 0 && a[l] == a[k - 1]) --l;
                while (r < n && a[r] == a[k - 1]) ++r;
                int N = r - l - 1, K = k - l - 1;
                output.Append(fact[N] * inv_fact[K] % mod * inv_fact[N - K] % mod).AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static long Inv(long a, long mod) {
            long u = 0, v = 1, m = mod;
            while (a != 0) {
                long t = m / a;
                m -= t * a; (m, a) = (a, m);
                u -= t * v; (u, v) = (v, u);
            }
            return (u % mod + mod) % mod;
        }
    }
}