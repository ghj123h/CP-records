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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>();
                (int l, int r)[] begin = new (int l, int r)[n];
                (int r, int l)[] end = new (int r, int l)[n];
                for (int i = 0; i < n; ++i) {
                    int l = Read<int>(), r = Read<int>();
                    begin[i] = (l, r);
                    end[i] = (r, l);
                }
                Array.Sort(begin);
                Array.Sort(end);
                int ans = int.MaxValue;
                for (int i = 0; i < n; ++i) {
                    var j = Array.BinarySearch(begin, (begin[i].r + 1, 0));
                    var k = Array.BinarySearch(end, (begin[i].l, 0));
                    j = ~j; k = ~k;
                    ans = Math.Min(ans, n - j + k);
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}