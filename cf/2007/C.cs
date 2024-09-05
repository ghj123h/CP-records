using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace Template
{
    internal class SolutionC
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
                int n = Read<int>(), a = Read<int>(), b = Read<int>();
                long[] c = ReadArray<long>(n);
                int g = gcd(a, b);
                for (int i = 0; i < n; ++i) c[i] %= g;
                Array.Sort(c);
                long ans = c[^1] - c[0];
                for (int i = 1; i < n; ++i) ans = Math.Min(ans, g - (c[i] - c[i - 1]));
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static int gcd(int a, int b) => b == 0 ? a : gcd(b, a % b);
    }
}