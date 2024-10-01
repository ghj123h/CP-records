using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateD2;

#if !PROBLEM
SolutionD2 a = new();
a.Solve();
#endif

namespace TemplateD2
{
    internal class SolutionD2
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
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                Array.Sort(a);
                int ans = 0;
                for (int i = 0; i <= n / 2; ++i) ans = Math.Max(ans, Sub(a.AsSpan().Slice(i), n / 2));
                output.Append(ans == int.MaxValue ? -1 : ans).AppendLine();
            }
            Console.Write(output.ToString());
        }

        static int Gcd(int a, int b) => a == 0 ? b : (b == 0 ? a : Gcd(b, a % b));
        static int Sub(Span<int> a, int m) {
            int n = a.Length, zero = 1;
            for (int i = 1; i < n; ++i) {
                a[i] -= a[0];
                if (a[i] == 0) ++zero;

            }
            a[0] = 0;
            if (zero >= m) return int.MaxValue;
            int[] f = new int[a[^1] + 1];
            for (int i = 1; i < n; ++i) {
                if (a[i] != 0) {
                    for (int j = 1; j * j <= a[i]; ++j) {
                        if (a[i] % j == 0) {
                            ++f[j];
                            if (j * j != a[i]) ++f[a[i] / j];
                        }
                    }
                }
            }
            for (int k = a[^1]; k >= 0; --k) {
                if (f[k] >= m - zero) return k;
            }
            return int.MaxValue;
        }
    }
}