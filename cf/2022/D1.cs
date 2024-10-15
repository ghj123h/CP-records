using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateD1;

#if !PROBLEM
SolutionD1 a = new();
a.Solve();
#endif

namespace TemplateD1
{
    internal class SolutionD1
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
                for (int i = 1; i <= n; i += 2) {
                    if (i == n) {
                        Console.WriteLine("! {0}", i);
                        Console.Out.Flush();
                        break;
                    }
                    if (!Check(i, i + 1)) {
                        Console.WriteLine("! {0}", Check(i, i == 1 ? n : i - 1) ? i + 1 : i);
                        Console.Out.Flush();
                        break;
                    }
                }
            }
        }

        public bool Check(int u, int v) {
            Console.WriteLine("? {0} {1}", u, v);
            Console.Out.Flush();
            int a = Read<int>();
            Console.WriteLine("? {0} {1}", v, u);
            Console.Out.Flush();
            int b = Read<int>();
            return a == b;
        }
    }
}