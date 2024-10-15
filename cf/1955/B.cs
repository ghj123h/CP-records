using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB;

#if !PROBLEM
SolutionB a = new();
a.Solve();
#endif

namespace TemplateB
{
    internal class SolutionB
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
                int c = Read<int>(), d = Read<int>();
                int[] b = ReadArray<int>(n * n);
                Array.Sort(b);
                int[] a = new int[n * n];
                a[0] = b[0];
                for (int i = 0; i < n; ++i) {
                    if (i > 0) a[i * n] = a[(i - 1) * n] + c;
                    for (int j = 1; j < n; ++j) {
                        a[i * n + j] = a[i * n + j - 1] + d;
                    }
                }
                Array.Sort(a);
                output.AppendLine(a.SequenceEqual(b) ? "Yes" : "No");
            }
            Console.Write(output.ToString());
        }
    }
}