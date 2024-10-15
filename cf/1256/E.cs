using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            int n = Read<int>();
            int[] a = ReadArray<int>(n);
            int[] idx = Enumerable.Range(0, n).OrderBy(i => a[i]).ToArray();
            int[] d = new int[n + 1], pre = new int[n + 1], mxi = new int[n + 1];
            int mx = 0;
            for (int i = 2; i < n - 3; ++i) {
                if (d[i] < mx + a[idx[i+1]] - a[idx[i]]) {
                    pre[i + 1] = mxi[i - 2];
                    d[i + 1] = mx + a[idx[i+1]] - a[idx[i]];
                } else {
                    d[i + 1] = d[i];
                    pre[i + 1] = -1;
                }
                if (mx > d[i-1]) {
                    mxi[i - 1] = mxi[i - 2];
                } else {
                    mx = d[i - 1];
                    mxi[i - 1] = i - 1;
                }
            }
            List<int> cut = new();
            for (int i = n - 3; i >= 3; ) {
                if (pre[i] >= 0) {
                    cut.Add(i);
                    i = pre[i];
                } else --i;
            }
            cut.Reverse();
            int[] ans = new int[n];
            output.AppendFormat("{0} {1}\n", a[idx[^1]] - a[idx[0]] - d[^4], cut.Count + 1);
            for (int i = 0, j = 1, k = 0; i < n; ++i) {
                if (k < cut.Count && i >= cut[k]) {
                    ++j; ++k;
                }
                ans[idx[i]] = j;
            }
            output.AppendJoin(' ', ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}