using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TemplateC1;

#if !PROBLEM
SolutionC1 a = new();
a.Solve();
#endif

namespace TemplateC1
{
    internal class SolutionC1
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
            while (T-- > 0) {
                int n = Read<int>(), ans = 0;
                long m = Read<long>();
                int m2 = (int)Math.Min(m, (1L << 32 - BitOperations.LeadingZeroCount((uint)n)) - 1);
                for (int j = 1; j * j <= n; ++j) {
                    if (n % j == 0) {
                        sub(n, j);
                        if (j * j < n) sub(n, n / j);
                    }
                }
                for (int i = 1; i <= m2; ++i) {
                    for (int j = i; j <= m2; j += i) {
                        if ((j ^ i) == n) ++ans;
                    }
                }
                output.Append(ans).AppendLine();
                

                void sub(int x, int div) {
                    int y = x ^ div;
                    if (y <= m && y % div != 0) ++ans;
                }
            }
            Console.Write(output.ToString());
        }
    }
}