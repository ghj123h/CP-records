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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                int cnt = 0, p = 0, l = 0;
                var idx = Enumerable.Range(0, n).OrderBy(i => a[i]).ToArray();
                long sum = 0;
                for (int i = 0; i < n; ++i) {
                    int j = idx[i];
                    if (p != a[j]) {
                        if (p > 0) {
                            sum += 1L * cnt * p;
                            if (sum < a[j]) l = i;
                        }
                        p = a[j];
                        cnt = 1;
                    } else ++cnt;
                }
                output.Append(n - l).AppendLine();
                output.AppendJoin(' ', idx.Skip(l).Select(x => x + 1).OrderBy(x => x)).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}