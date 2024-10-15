using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateC;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace TemplateC
{
    internal class SolutionC
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
                if (n == 1) output.AppendLine("1");
                else if (n == 2) output.AppendLine("-1");
                else {
                    int[,] ans = new int[n, n];
                    ans[0, 0] = 1;
                    ans[n - 1, n - 1] = 2;
                    int v = 3;
                    for (int k = 1; k <= 2 * n - 3; ++k) {
                        for (int i = Math.Max(0, k - n + 1); i < n && k - i >= 0; ++i) {
                            ans[i, k - i] = v++;
                        }
                    }
                    for (int i = 0;i <n; ++i) {
                        for (int j = 0; j < n; ++j) output.AppendFormat("{0} ", ans[i, j]);
                        output.AppendLine();
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}