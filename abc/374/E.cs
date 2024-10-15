using System;
using System.Collections.Generic;
using System.Linq;
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
            int n = Read<int>(), x = Read<int>();
            int[] A = new int[n], B = new int[n], P = new int[n], Q = new int[n];
            for (int i = 0; i < n; ++i) {
                A[i] = Read<int>();
                P[i] = Read<int>();
                B[i] = Read<int>();
                Q[i] = Read<int>();
            }
            int l = 1, r = int.MaxValue;
            while (l < r) {
                int m = l + (r - l) / 2;
                long sum = 0;
                for (int i = 0; i < n; ++i) {
                    int a = A[i], b = B[i], p = P[i], q = Q[i];
                    if (p * b > a * q) {
                        (a, b) = (b, a);
                        (p, q) = (q, p);
                    }
                    long na = (m + a - 1) / a;
                    long c = na * p;
                    for (long j = na - 1; j >= 0 && j >= na - b; --j) {
                        c = Math.Min(c, j * p + (m - j * a + b - 1) / b * q);
                    }
                    sum += c;
                }
                if (sum <= x) l = m + 1;
                else r = m;
            }
            output.Append(l - 1).AppendLine();
            Console.Write(output.ToString());
        }
    }
}