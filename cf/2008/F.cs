using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateF;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace TemplateF
{
    internal class SolutionF
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
                long[] a = ReadArray<long>(n);
                long q = Inv(1L * n * (n - 1) / 2 % Mod);
                long sum = a.Sum() % Mod;
                sum *= sum;
                for (int i = 0; i < n; ++i)
                {
                    sum -= a[i] * a[i] % Mod;
                    sum %= Mod;
                    sum = (sum + Mod) % Mod;
                }
                sum *= Inv(2);
                sum %= Mod;
                output.Append(sum * q % Mod).AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static readonly int Mod = 1000000007;

        public static long Inv(long a)
        {
            long u = 0, v = 1, m = Mod;
            while (a > 0)
            {
                long t = m / a;
                m -= t * a; (a, m) = (m, a);
                u -= t * v; (u, v) = (v, u);
            }
            return (u % Mod + Mod) % Mod;
        }
    }
}