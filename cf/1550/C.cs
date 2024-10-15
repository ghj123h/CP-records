using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateC;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace TemplateC
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
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                long ans = 0;
                for (int r = 0; r < n; ++r) {
                    if (r < 2) ans += r + 1;
                    else if (Bad(a[r - 2], a[r - 1], a[r])) ans += 2;
                    else if (r == 2 || Bad(a[r - 3], a[r - 2], a[r - 1]) || Bad(a[r - 3], a[r - 2], a[r]) || Bad(a[r - 3], a[r - 1], a[r])) ans += 3;
                    else ans += 4;
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static bool Bad(int a, int b, int c) {
            return (a <= b && b <= c) || (a >= b && b >= c);
        }
    }
}