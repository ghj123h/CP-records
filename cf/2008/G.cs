using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateG;

#if !PROBLEM
SolutionG a = new();
a.Solve();
#endif

namespace TemplateG
{
    internal class SolutionG
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
                if (n == 1) output.Append(k > a[0] ? k : k - 1).AppendLine();
                else
                {
                    int g = a.Aggregate(a[0], (a, b) => Gcd(a, b));
                    if (g == 0) output.Append(k).AppendLine();
                    else if (g == 1) output.Append(n + k - 1).AppendLine();
                    else
                    {
                        int gap = (k - 1) / (g - 1);
                        if (gap <= n - 1)
                        {
                            output.Append(gap * g + (k - 1) % (g - 1) + 1).AppendLine();
                        }
                        else
                        {
                            k -= (n - 1) * (g - 1);
                            output.Append((n - 1) * g + k).AppendLine();
                        }
                    }
                }
            }
            Console.Write(output.ToString());
        }

        public static int Gcd(int a, int b) => b == 0 ? a : Gcd(b, a % b);
    }
}