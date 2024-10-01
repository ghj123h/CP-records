using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateC;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace TemplateC
{
    internal class SolutionC
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
                int a = Read<int>(), b = Read<int>(), k = Read<int>();
                int[] A = ReadArray<int>(k), B = ReadArray<int>(k);
                int[] ma = new int[a + 1], mb = new int[b + 1];
                ma[A[0]]++; mb[B[0]]++;
                long ans = 0;
                for (int i = 1; i < k; ++i) {
                    ans += i - ma[A[i]] - mb[B[i]];
                    ma[A[i]]++;
                    mb[B[i]]++;
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}