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
                int[] p = ReadArray<int>(n);
                int[] ans = new int[n];
                dfs(0, n, 0);
                output.AppendJoin(' ', ans).AppendLine();
                
                void dfs(int l, int r, int d) {
                    if (l >= r) return;
                    int mi = l, max = p[l];
                    for (int i = l + 1; i < r; ++i) if (max < p[i]) { max = p[i]; mi = i; }
                    ans[mi] = d;
                    dfs(l, mi, d + 1);
                    dfs(mi + 1, r, d + 1);
                }
            }
            Console.Write(output.ToString());
        }
    }
}