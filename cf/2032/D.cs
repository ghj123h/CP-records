using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TemplateD;

#if !PROBLEM
SolutionD a = new();
a.Solve();
#endif

namespace TemplateD
{
    internal class SolutionD
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
                int l = n - 2, r = n - 1;
                int[] fa = new int[n];
                while (l > 0) {
                    Console.WriteLine("? {0} {1}", l, r);
                    Console.Out.Flush();
                    int res = Read<int>();
                    if (res == 0) fa[r--] = l--;
                    else --l;
                }
                StringBuilder output = new("! ");
                output.AppendJoin(' ', fa.Skip(1));
                Console.WriteLine(output.ToString());
            }
        }
    }
}