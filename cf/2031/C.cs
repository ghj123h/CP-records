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
                if (n % 2 == 1 && n <= 25) {
                    output.AppendLine("-1");
                    continue;
                } 
                if (n % 2 == 1) {
                    output.Append("1 2 2 3 3 4 4 5 5 1 6 6 7 7 8 8 9 9 11 11 12 12 10 13 13 1 10");
                    for (int i = 14; i <= n / 2; ++i) output.AppendFormat(" {0} {1}", i, i);
                    output.AppendLine();
                } else {
                    for (int i = 1; i <= n / 2; ++i) output.AppendFormat("{0} {1} ", i, i);
                    output.AppendLine();
                }
            }
            Console.Write(output.ToString());
        }
    }
}