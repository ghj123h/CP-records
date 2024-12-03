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
                int c = 0;
                for (int i = 0; i + 1 < n / 2; ++i) {
                    c += Math.Min(
                        Convert.ToInt32(a[i] == a[n - i - 2]) + Convert.ToInt32(a[n - i - 1] == a[i + 1]),
                        Convert.ToInt32(a[n - i - 1] == a[n - i - 2]) + Convert.ToInt32(a[i] == a[i + 1]));
                }
                if (n % 2 == 0) {
                    output.Append(c + Convert.ToInt32(a[n / 2 - 1] == a[n / 2]));
                } else {
                    output.Append(c + Convert.ToInt32(a[n / 2 - 1] == a[n / 2]) + Convert.ToInt32(a[n / 2] == a[n / 2 + 1]));
                }
                output.AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}