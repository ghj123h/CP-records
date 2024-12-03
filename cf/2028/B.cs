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
                long n = Read<long>(), b = Read<long>(), c = Read<long>();
                if (b == 0) {
                    if (c == n - 2 || c == n - 1) output.Append(n - 1);
                    else if (c >= n) output.Append(n);
                    else output.Append(-1);
                } else {
                    // b(i-1)+c <= n-1
                    // i <= (n-1-c+b)/b
                    long u = (n - 1 - c + b) / b;
                    u = Math.Max(u, 0);
                    output.Append(n - u);
                }
                output.AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}