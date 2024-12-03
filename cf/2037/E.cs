using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>();
                Console.WriteLine("? 1 {0}", n);
                Console.Out.Flush();
                long tot = Read<long>();
                if (tot == 0) {
                    Console.WriteLine("! IMPOSSIBLE");
                    Console.Out.Flush();
                } else {
                    StringBuilder sb = new(n);
                    sb.AppendJoin("", Enumerable.Repeat('0', n));
                    for (int i = n - 1; i >= 1 && tot > 0; --i) {
                        long cur;
                        if (i > 1) {
                            Console.WriteLine("? 1 {0}", i);
                            Console.Out.Flush();
                            cur = Read<long>();
                        } else cur = 0;
                        if (tot > cur) sb[i] = '1';
                        if (cur == 0) {
                            for (int j = 0; j < (int)(i - tot); ++j) sb[j] = '1';
                        }
                        tot = cur;
                    }
                    Console.WriteLine("! {0}", sb.ToString());
                    Console.Out.Flush();
                }
            }
        }
    }
}