using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TemplateF1;

#if !PROBLEM
SolutionF1 a = new();
a.Solve();
#endif

namespace TemplateF1
{
    internal class SolutionF1
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
            int n = Read<int>(), t = Read<int>();
            int k = Read<int>();
            int l = 1, r = n + 1;
            while (l < r) {
                int m = l + (r - l) / 2;
                Console.WriteLine("? {0} {1}", 1, m);
                Console.Out.Flush();
                int q = Read<int>();
                if (m - q >= k) r = m;
                else l = m + 1;
            }
            Console.WriteLine("! {0}", l);
            Console.Out.Flush();
        }
    }
}