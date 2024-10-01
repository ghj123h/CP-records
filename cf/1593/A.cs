using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TemplateA;

#if !PROBLEM
SolutionA a = new();
a.Solve();
#endif

namespace TemplateA
{
    internal class SolutionA
    {
        private readonly StreamReader sr = new(Console.OpenStandardInput());
        private T Read<T>()
            where T: struct, IConvertible
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
                int a = Read<int>(), b = Read<int>(), c = Read<int>();
                if (a == b && b == c) output.AppendLine("1 1 1");
                else if (a == b && b > c) output.AppendFormat("1 1 {0}\n", a + 1 - c);
                else if (a == c && c > b) output.AppendFormat("1 {0} 1\n", a + 1 - b);
                else if (b == c && c > a) output.AppendFormat("{0} 1 1\n", b + 1 - a);
                else if (a > b && a > c) output.AppendFormat("0 {0} {1}\n", a + 1 - b, a + 1 - c);
                else if (b > a && b > c) output.AppendFormat("{0} 0 {1}\n", b + 1 - a, b + 1 - c);
                else if (c > a && c > b) output.AppendFormat("{0} {1} 0\n", c + 1 - a, c + 1 - b);
            }
            Console.Write(output.ToString());
        }
    }
}