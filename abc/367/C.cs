using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace Template
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
            // int T = Read<int>();
            int T = 1;
            while (T-- > 0)
            {
                int n = Read<int>(), k = Read<int>();
                int[] r = ReadArray<int>(n);
                int[] cur = new int[n];
                dfs(0, 0);

                void dfs(int i, int sum)
                {
                    if (i == n)
                    {
                        if (sum % k == 0) output.AppendJoin(' ', cur).AppendLine();
                        return;
                    }
                    for (int c = 1; c <= r[i]; ++c)
                    {
                        cur[i] = c;
                        dfs(i + 1, sum + c);
                    }
                }
            }
            Console.WriteLine(output.ToString());
        }
    }
}