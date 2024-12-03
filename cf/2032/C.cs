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
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                int max = 0;
                Array.Sort(a);
                for (int l = 0, r = 2; r < n; ++r) {
                    while (a[l] + a[l + 1] <= a[r] && l < r - 1) ++l;
                    max = Math.Max(max, r - l + 1);
                }
                output.Append(n - max).AppendLine();
            }
            Console.Write(output.ToString());

            int LowerBound(IList<int> A, int u) {
                int L = 0, R = A.Count;
                while (L < R) {
                    int m = L + (R - L) / 2;
                    if (A[m] < u) L = m + 1;
                    else R = m;
                }
                return L;
            }
        }
    }
}