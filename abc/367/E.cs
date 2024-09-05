using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionE a = new();
a.Solve();
#endif

namespace Template
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

        private T[] ReadArray<T>(int n, int startIndex = 1)
            where T : struct, IConvertible
        {
            T[] arr = new T[n + startIndex];
            for (int i = startIndex; i < n + startIndex; ++i) arr[i] = Read<T>();
            return arr;
        }

        public void Solve()
        {
            StringBuilder output = new();
            // int T = Read<int>();
            int T = 1;
            while (T-- > 0)
            {
                int n = Read<int>();
                long k = Read<long>();
                int[] x = ReadArray<int>(n, 1), y = new int[n + 1];
                int[] a = ReadArray<int>(n, 1), b = new int[n + 1];
                while (k > 0)
                {
                    if (k % 2 > 0)
                    {
                        for (int i = 1; i <= n; ++i) b[i] = a[x[i]];
                        (a, b) = (b, a);
                    }
                    for (int i = 1; i <= n; ++i) y[i] = x[x[i]];
                    (x, y) = (y, x);
                    k /= 2;
                }
                output.AppendJoin(' ', a.Skip(1));
            }
            Console.WriteLine(output.ToString());
        }
    }
}