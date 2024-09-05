using System;
using System.Collections.Generic;
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
                int n = Read<int>();
                bool[,] vis = new bool[n + 1, 2];
                for (int i = 0; i < n; ++i)
                {
                    int x = Read<int>(), y = Read<int>();
                    vis[x, y] = true;
                }
                long ans = 0;
                for (int i = 0; i <= n; ++i)
                {
                    if (vis[i, 0] && vis[i, 1]) ans += n - 2;
                }
                for (int i = 1; i < n; ++i)
                {
                    if (vis[i, 0] && vis[i - 1, 1] && vis[i + 1, 1]) ++ans;
                    if (vis[i, 1] && vis[i - 1, 0] && vis[i + 1, 0]) ++ans;
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}