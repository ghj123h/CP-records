using System;
using System.Collections.Generic;
using System.IO.Pipes;
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
                int n  = Read<int>();
                var a = ReadArray<int>(n).ToList();
                int ans = int.MaxValue;
                int[] d = new int[n];
                Array.Fill(d, 1);
                for (int i = 1; i < n; ++i) for (int j = i - 1; j >= 0; --j) if (a[j] >= a[i]) d[i] = Math.Max(d[i], d[j] + 1);
                for (int i = 0; i < n; ++i) {
                    int c = 0;
                    for (int j = 0; j < n; ++j) {
                        if (j > i && a[j] > a[i]) ++c;
                    }
                    ans = Math.Min(ans, c + i + 1 - d[i]);
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}