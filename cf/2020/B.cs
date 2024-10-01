using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB;

#if !PROBLEM
SolutionB a = new();
a.Solve();
#endif

namespace TemplateB
{
    internal class SolutionB
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
            while (T-- > 0) {
                long k = Read<long>();
                long n = SqrtL(k);
                k += n;
                long v = 0;
                do {
                    v = SqrtL(k);
                    k += v - n;
                    n = v;
                } while (v > n);
                output.Append(k).AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static long SqrtL(long u) {
            long L = 0, R = int.MaxValue / 2;
            while (L < R) {
                long m = L + (R - L) / 2;
                if (m * m <= u) L = m + 1;
                else R = m;
            }
            return L - 1;
        }
    }
}