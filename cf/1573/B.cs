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
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                List<(int, int, int)> ans = new();
                for (int i = 0; i < n - 1; ++i) {
                    int min = a[i], mini = i;
                    for (int j = i + 1; j < n; ++j) if (a[j] < min) min = a[mini = j];
                    if (mini > i) {
                        ans.Add((i + 1, mini + 1, mini - i));
                        for (int j = mini; j > i; --j) a[j] = a[j - 1];
                        a[i] = min;
                    }
                }
                output.Append(ans.Count).AppendLine();
                foreach (var (l, r, d) in ans) output.AppendFormat("{0} {1} {2}\n", l, r, d);
            }
            Console.Write(output.ToString());
        }
    }
}