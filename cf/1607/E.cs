using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE;

#if !PROBLEM
SolutionE a = new();
a.Solve();
#endif

namespace TemplateE
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
                int n = Read<int>(), m = Read<int>();
                sr.ReadLine();
                string s = sr.ReadLine();
                int x = 0, y = 0;
                int l = 0, r = 0, u = 0, b = 0;
                foreach (var c in s) {
                    switch (c) {
                        case 'L':
                            if (x - 1 < l) {
                                if (r - x + 2 > m) goto end;
                                else l = --x;
                            } else --x;
                            break;
                        case 'R':
                            if (x + 1 > r) {
                                if (x - l + 2 > m) goto end;
                                else r = ++x;
                            } else ++x;
                            break;
                        case 'U':
                            if (y - 1 < u) {
                                if (b - y + 2 > n) goto end;
                                else u = --y;
                            } else --y;
                            break;
                        case 'D':
                            if (y + 1 > b) {
                                if (y - u + 2 > n) goto end;
                                else b = ++y;
                            } else ++y;
                            break;
                    }
                }
            end:
                output.AppendFormat("{0} {1}\n", 1 - u, 1 - l);
            }
            Console.Write(output.ToString());
        }
    }
}