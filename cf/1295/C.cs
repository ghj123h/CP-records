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
            sr.ReadLine();
            while (T-- > 0)
            {
                string s = sr.ReadLine(), t = sr.ReadLine();
                int n = s.Length, m = t.Length;
                int[,] d = new int[n, 26];
                for (char c = 'a'; c <= 'z'; ++c) {
                    for (int i = 0; i < n; ++i) d[i, c - 'a'] = -1;
                    int j = s.IndexOf(c);
                    if (j >= 0) {
                        int k = j, i = j;
                        do {
                            d[i, c - 'a'] = k;
                            i = (i + n - 1) % n;
                            if (s[i] == c) k = i;
                        } while (i != j);
                    }
                }
                int ans = 1;
                for (int i = 0, j = 0; j < m; ++j) {
                    if (i == n) {
                        ++ans;
                        i = 0;
                    }
                    int k = d[i, t[j] - 'a'];
                    if (k == -1) {
                        ans = -1;
                        break;
                    }
                    if (k < i) ++ans;
                    i = k + 1;
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}