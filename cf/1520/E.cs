using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE;

#if !PROBLEM
SolutionE a = new();
a.Solve();
#endif

namespace TemplateE
{
    internal class SolutionE
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
                List<(int l, int r)> vals = new();
                int n = Read<int>();
                sr.ReadLine();
                string s = sr.ReadLine();
                int l = -1, r = 0;
                for (int i = 0; i < n; ++i) {
                    if (s[i] == '*') {
                        if (l < 0) l = i;
                        r = i;
                    } else if (l >= 0) {
                        vals.Add((l, r));
                        l = -1;
                    }
                }
                if (l >= 0) vals.Add((l, r));
                int m = vals.Count;
                long ans = 0, gap = 0;
                int[] pre = new int[m + 1];
                for (int i = 0; i < m; ++i) pre[i + 1] = pre[i] + (vals[i].r - vals[i].l + 1);
                for (int i = 1; i < m; ++i) {
                    gap += vals[i].l - vals[i - 1].r - 1;
                    ans += gap * (vals[i].r - vals[i].l + 1);
                }
                long c = ans;
                for (int i = 1; i < m; ++i) {
                    c += 1L * pre[i] * (vals[i].l - vals[i - 1].r - 1);
                    c -= 1L * (pre[m] - pre[i]) * (vals[i].l - vals[i - 1].r - 1);
                    ans = Math.Min(c, ans);
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}