using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
                int n = Read<int>(), m = Read<int>();
                int[] a = ReadArray<int>(n);
                List<(long v, int i)> inc = new();
                long sum = 0, max = 0;
                inc.Add((0, 0));
                for (int i = 0; i < n; ++i) {
                    sum += a[i];
                    if (sum > max) {
                        max = sum;
                        inc.Add((max, i + 1));
                    }
                }
                long[] ans = new long[m];
                for (int i = 0; i < m; ++i) {
                    int x = Read<int>();
                    if (x > max) {
                        if (sum <= 0) ans[i] = -1;
                        else {
                            long ex = (x - max + sum - 1) / sum;
                            long rem = x - ex * sum;
                            var j = inc.BinarySearch((rem, 0));
                            if (j < 0) j = ~j;
                            ans[i] = ex * n + inc[j].i - 1;
                        }
                    } else {
                        var j = inc.BinarySearch((x, 0));
                        if (j < 0) j = ~j;
                        ans[i] = inc[j].i - 1;
                    }
                }
                output.AppendJoin(' ', ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}