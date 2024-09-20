using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
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
            int n = Read<int>();
            int[] a = ReadArray<int>(n);
            long[] sum = new long[n + 1];
            long d = 0, ans = 0;
            for (int i = 0; i < n; ++i)
            {
                d += i + 1 - sum[a[i]];
                sum[a[i]] = i + 1;
                ans += d;
            }
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}