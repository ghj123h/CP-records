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
                int n = Read<int>(), m = Read<int>();
                int[,] v = new int[n, 5], next = new int[n, 5], d = new int[n + 1, 5];
                string p = "narek";
                sr.ReadLine();
                for (int i = 0; i < n; ++i)
                {
                    string s = sr.ReadLine();
                    int sum = s.Count(c => p.Contains(c));
                    for (int j = 0; j < 5; ++j)
                    {
                        int l = j;
                        for (int k = 0; k < m; ++k)
                        {
                            if (p[l] == s[k])
                            {
                                l = (l + 1) % 5;
                                if (l == 0) v[i, j] += 10;
                            }
                        }
                        v[i, j] -= sum;
                        next[i, j] = l;
                    }
                }
                d[0, 0] = 0;
                d[0, 1] = d[0, 2] = d[0, 3] = d[0, 4] = -0x3f3f3f3f;
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < 5; ++j) d[i + 1, j] = d[i, j];
                    for (int j = 0; j < 5; ++j) d[i + 1, next[i, j]] = Math.Max(d[i + 1, next[i, j]], d[i, j] + v[i, j]);
                }
                int ans = 0;
                for (int j = 0; j < 5; ++j) ans = Math.Max(d[n, j], ans);
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}