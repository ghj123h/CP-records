using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TemplateA;

#if !PROBLEM
SolutionA a = new();
a.Solve();
#endif

namespace TemplateA
{
    internal class SolutionA
    {
        private readonly StreamReader sr = new(Console.OpenStandardInput());
        private T Read<T>()
            where T: struct, IConvertible
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
            int n = Read<int>(), c = Read<int>();
            int[] a = ReadArray<int>(n);
            int l = -10000000, ans = 0;
            for (int i = 0; i < n; ++i) {
                if (a[i] - l >= c) {
                    ++ans;
                    l = a[i];
                }
            }
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}