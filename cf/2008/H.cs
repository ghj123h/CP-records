using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateH;

#if !PROBLEM
SolutionH a = new();
a.Solve();
#endif

namespace TemplateH
{
    internal class SolutionH
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
                int n = Read<int>(), q = Read<int>();
                int[] a = ReadArray<int>(n);
                int[] ans = new int[n + 1];
                int[] f = new int[n + 1], pre = new int[n * 2 + 1];
                Array.Fill(ans, -1);
                for (int i = 0; i < n; ++i) f[a[i]]++;
                for (int i = 1; i <= n; ++i) pre[i] = pre[i - 1] + f[i];
                for (int i = n + 1; i <= 2 * n; ++i) pre[i] = pre[i - 1];
                while (q-- > 0)
                {
                    int x = Read<int>();
                    if (ans[x] < 0)
                    {
                        int l = 0, r = x;
                        while (l < r)
                        {
                            int m = l + (r - l) / 2;
                            int sum = pre[m];
                            for (int i = x; i <= n; i += x)
                            {
                                sum += pre[i + m] - pre[i - 1];
                            }
                            if (sum <= n / 2) l = m + 1;
                            else r = m;
                        }
                        ans[x] = l;
                    }
                    output.AppendFormat("{0} ", ans[x]);
                }
                output.AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}