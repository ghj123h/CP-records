using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateH;

#if !PROBLEM
SolutionH a = new();
a.Solve();
#endif

namespace TemplateH
{
    internal class SolutionH
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
                int n = Read<int>(), k = Read<int>();
                int[] a = ReadArray<int>(n);
                int[] d = new int[31];
                int res = 0;
                for (int j = 0; j <= 30; ++j) for (int i = 0; i < n; ++i) if (((a[i] >> j) & 1) == 0) d[j]++;
                for (int j = 30; j >= 0; --j) {
                    if (k >= d[j]) {
                        k -= d[j];
                        res ^= 1 << j;
                    }
                }
                output.Append(res).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}