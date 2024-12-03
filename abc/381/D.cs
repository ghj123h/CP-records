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
            int n = Read<int>();
            int[] a = ReadArray<int>(n);
            int[] mp = new int[n + 1];
            int ans = 0;
            for (int k = 0; k < 2; ++k) {
                int res = 0;
                for (int r = k + 1, l = k; r < n; r += 2) {
                    if (a[r] != a[r-1]) {
                        while (l < r) {
                            mp[a[l]] = 0;
                            l += 2;
                        }
                        continue;
                    }
                    while (mp[a[r]] > 0) {
                        mp[a[l]]--;
                        l += 2;
                    }
                    ++mp[a[r]];
                    res = Math.Max(res, r - l + 1);
                }
                Array.Fill(mp, 0);
                ans = Math.Max(ans, res);
            }
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}