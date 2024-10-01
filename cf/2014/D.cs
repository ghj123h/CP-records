using System;
using System.Collections.Generic;
using System.IO.Pipes;
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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>(), d = Read<int>(), k = Read<int>();
                int[] l = new int[n + 1], r = new int[n + 1];
                for (int i = 0; i < k; ++i) {
                    ++l[Read<int>()];
                    ++r[Read<int>()];
                }
                int cur = 0;
                for (int i = 1; i <= d; ++i) cur += l[i];
                int max = cur, ans1 = d, min = cur, ans2 = d;
                for (int i = d + 1; i <= n; ++i) {
                    cur += l[i] - r[i - d];
                    if (max < cur) {
                        max = cur;
                        ans1 = i;
                    }
                    if (min > cur) {
                        min = cur;
                        ans2 = i;
                    }
                }
                output.AppendFormat("{0} {1}\n", ans1 - d + 1, ans2 - d + 1);
            }
            Console.Write(output.ToString());
        }
    }
}