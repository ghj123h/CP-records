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
            // int T = Read<int>();
            int T = 1;
            while (T-- > 0)
            {
                int n = Read<int>(), a = Read<int>(), b = Read<int>(), k = Read<int>();
                int[] h = ReadArray<int>(n);
                for (int i = 0; i < n; ++i) {
                    h[i] = h[i] % (a + b);
                    if (h[i] == 0) h[i] = a + b;
                    h[i] = (h[i] + a - 1) / a - 1;
                }
                Array.Sort(h);
                int ans;
                for (ans = 0; ans < n; ++ans) {
                    if (h[ans] > k) break;
                    else k -= h[ans];
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}