using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            StringBuilder output = new();
            int n = Read<int>();
            int[] x = ReadArray<int>(n), p = ReadArray<int>(n);
            long[] pre = new long[n + 1];
            for (int i = 0; i < n; ++i) pre[i + 1] = pre[i] + p[i];
            int q = Read<int>();
            while (q-- > 0)
            {
                int l = Read<int>(), r = Read<int>();
                int i = Array.BinarySearch(x, l), j = Array.BinarySearch(x, r);
                if (i < 0) i = ~i;
                if (j < 0) j = ~j; else ++j;
                output.Append(pre[j] - pre[i]).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}