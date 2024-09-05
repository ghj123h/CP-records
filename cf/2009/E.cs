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
            while (T-- > 0)
            {
                long n = Read<long>(), k = Read<long>();
                long L = 0, R = n;
                long tar = n * (2 * k + n - 1) / 2;
                while (L < R)
                {
                    long m = L + (R - L) / 2;
                    if (check(k, m) < tar) L = m + 1;
                    else R = m;
                }
                output.Append(Math.Min(Math.Abs(check(k, L) - tar), Math.Abs(check(k, L - 1) - tar))).AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static long check(long k, long m) => (2 * k + m) * (m + 1);
    }
}