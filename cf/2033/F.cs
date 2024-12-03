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
            // int T = Read<int>();
            int T = 1;
            int mod = 1000000007;
            while (T-- > 0)
            {
                long n = Read<long>();
                // int k = Read<int>();
                // if (k == 1) output.Append(n % mod).AppendLine();
                // else {
                for (int k = 2; k <= 100000; ++k) {
                    int f1 = 1, f2 = 1, f3 = 1, g = 2;
                    while (f3 != 0) {
                        f3 = (f1 + f2) % k;
                        f1 = f2;
                        f2 = f3;
                        ++g;
                    }
                    // if (k % 100 == 0) output.AppendFormat("k = {0}", k).AppendLine();
                    if (g >= 2 * k) output.Append(k).AppendLine();
                }
                    // output.Append(n % mod * g % mod).AppendLine();
                // }
            }
            Console.Write(output.ToString());
        }
    }
}