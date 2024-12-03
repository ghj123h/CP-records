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
                int[] a = ReadArray<int>(n);
                int[] cnt = new int[3];
                for (int i = 0; i < n; ++i) cnt[a[i]]++;
                int r = n - 1, l = 0, o = Array.IndexOf(a, 1);
                List<(int, int)> ans = new();
                for (; r >= n - cnt[2]; --r) {
                    if (a[r] != 2) {
                        while (a[l] != 2) ++l;
                        if (a[r] != 1) {
                            ans.Add((o + 1, r + 1));
                            (a[o], a[r]) = (a[r], a[o]);
                        }
                        o = r;
                        ans.Add((o + 1, l + 1));
                        (a[o], a[l]) = (a[l], a[o]);
                        o = l++;
                    }
                }
                l = 0;
                for (; r >= n - cnt[2] - cnt[1]; --r) {
                    if (a[r] != 1) {
                        while (a[l] != 1) ++l;
                        ans.Add((l + 1, r + 1));
                        (a[l], a[r]) = (a[r], a[l]);
                        ++l;
                    }
                }
                output.Append(ans.Count).AppendLine();
                foreach (var (u, v) in ans) output.AppendFormat("{0} {1}\n", u, v);
            }
            Console.Write(output.ToString());
        }
    }
}