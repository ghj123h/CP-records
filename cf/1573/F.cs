using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateF;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace TemplateF
{
    internal class SolutionF
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
                int n = Read<int>(), d = Read<int>();
                int[] a = ReadArray<int>(n);
                bool suc = true;
                int g = Gcd(n, d), ans = 0;
                for (int i = 0; i < g; ++i) {
                    int j = i, cnt = 0, max = 0, t = 0;
                    do {
                        if (a[j] == 1) ++cnt;
                        else {
                            max = Math.Max(cnt, max);
                            cnt = 0;
                        }
                        j = (j + d) % n;
                        if (j == i) ++t;
                    } while (t < 2);
                    max = Math.Max(cnt, max);
                    if (cnt == 2 * n / g) { suc = false; break; } else ans = Math.Max(ans, max);
                }
                output.Append(suc ? ans : -1).AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static int Gcd(int a, int b) => b == 0 ? a : Gcd(b, a % b);
    }
}