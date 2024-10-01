using System;
using System.Collections.Generic;
using System.Linq;
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
                string[] grid = new string[n];
                sr.ReadLine();
                for (int i = 0; i < n; ++i) grid[i] = sr.ReadLine();
                StringBuilder[] ans = new StringBuilder[n];
                for (int i = 0; i < n; ++i) ans[i] = new(grid[i]);
                for (int j = 0; j < m; ++j) {
                    int p = n - 1, cnt = 0;
                    for (int i = n - 1; i >= 0; --i) {
                        ans[i][j] = '.';
                        if (grid[i][j] == 'o') {
                            for (int k = p; k > p - cnt; --k) ans[k][j] = '*';
                            p = i - 1;
                            ans[i][j] = 'o';
                            cnt = 0;
                        } else if (grid[i][j] == '*') ++cnt;
                    }
                    for (int k = p; k > p - cnt; --k) ans[k][j] = '*';
                }
                foreach (var s in ans) output.Append(s).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}