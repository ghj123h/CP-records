using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            int mod = 998244353;
            int n = Read<int>(), k = Read<int>();
            long[] fact = new long[4096], powk = new long[4096];
            fact[0] = powk[0] = 1;
            for (int i = 1; i <= 4000; ++i) {
                fact[i] = fact[i - 1] * i % mod;
                powk[i] = powk[i - 1] * k % mod;
            }
            long ans = 0;
            long[] d = new long[n];
            for (int i = k - 1; i < n; ++i) {
                d[i] = fact[k] * powk[i - k + 1] % mod;
                for (int j = i - k + 1; j < i; ++j) {
                    d[i] += mod - d[j] * fact[i - j] % mod;
                    d[i] %= mod;
                }
                ans += d[i] * powk[n - i - 1] % mod;
                ans %= mod;
            }
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}