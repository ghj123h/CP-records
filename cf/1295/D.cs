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
            while (T-- > 0)
            {
                long a = Read<long>(), m = Read<long>();
                long g = Gcd(a, m);
                output.Append(Euler(m / g)).AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);

        public static long Euler(long a) {
            long e = a;
            for (long u = 2; u * u <= a; ++u) {
                if (a % u == 0) {
                    e = e / u * (u - 1);
                    do {
                        a /= u;
                    } while (a % u == 0);
                }
            }
            if (a > 1) e = e / a * (a - 1);
            return e;
        }
    }
}