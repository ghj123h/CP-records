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
            int n = Read<int>();
            sr.ReadLine();
            string[] grid = new string[n];
            for (int i = 0; i < n; ++i) grid[i] = sr.ReadLine();
            // (x, y) -> (y, N+1-x) -> (N+1-x, N+1-y) -> (N+1-y, x) -> (x, y)
            var sb = grid.Select(s => new StringBuilder(s)).ToArray();
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < n; ++j) {
                    int c = Math.Min(Math.Min(i, n - i - 1), Math.Min(j, n - j - 1));
                    switch (c % 4) {
                        case 0:
                            sb[i][j] = grid[n - j - 1][i]; break;
                        case 1:
                            sb[i][j] = grid[n - i - 1][n - j - 1]; break;
                        case 2:
                            sb[i][j] = grid[j][n - i - 1]; break;
                    }
                }
                output.Append(sb[i]).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}