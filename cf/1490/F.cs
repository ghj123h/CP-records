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
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                Array.Sort(a);
                int p = a[0], cnt = 1;
                List<int> f = new();
                for (int i = 1; i < n; ++i) {
                    if (p != a[i]) {
                        f.Add(cnt);
                        p = a[i]; cnt = 1;
                    } else ++cnt;
                }
                f.Add(cnt);
                f.Sort();
                int m = f.Count, sum = f.Sum();
                int j = 0, ans = int.MaxValue;
                for (int c = 0; c <= f[^1]; ++c) {
                    while (j < m && f[j] < c) ++j;
                    ans = Math.Min(ans, sum - c * (m - j));
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}