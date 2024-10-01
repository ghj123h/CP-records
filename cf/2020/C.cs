using System;
using System.Collections.Generic;
using System.IO.Pipes;
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
                // d + (a & c) = (a | b)
                long b = Read<long>(), c = Read<long>(), d = Read<long>();
                int p = 0;
                long a = 0;
                bool suc = false;
                for (int j = 0; j < 63; ++j) {
                    int dd = (int)((d >> j) & 1), bb = (int)((b >> j) & 1), cc = (int)((c >> j) & 1);
                    suc = false;
                    for (int aa = 0; aa < 2; ++aa) {
                        if ((dd + p + (cc & aa)) % 2 == (bb | aa)) {
                            suc = true;
                            a ^= (1L * aa) << j;
                            p = (dd + p + (cc & aa)) / 2;
                            break;
                        }
                    }
                    if (!suc) break;
                }
                output.Append(suc ? a : -1).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}