using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateF;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace TemplateF {
    internal class SolutionF {
        private readonly StreamReader sr = new(Console.OpenStandardInput());
        private T Read<T>()
            where T : struct, IConvertible {
            char c;
            dynamic res = default(T);
            dynamic sign = 1;
            while (!sr.EndOfStream && char.IsWhiteSpace((char)sr.Peek())) sr.Read();
            if (!sr.EndOfStream && (char)sr.Peek() == '-') {
                sr.Read();
                sign = -1;
            }
            while (!sr.EndOfStream && char.IsDigit((char)sr.Peek())) {
                c = (char)sr.Read();
                res = res * 10 + c - '0';
            }
            return res * sign;
        }

        private T[] ReadArray<T>(int n, int startIndex = 0)
            where T : struct, IConvertible {
            T[] arr = new T[n + startIndex];
            for (int i = startIndex; i < n + startIndex; ++i) arr[i] = Read<T>();
            return arr;
        }

        public void Solve() {
            StringBuilder output = new();
            int n = Read<int>(), k = Read<int>();
            long[] a = ReadArray<long>(n);
            long[] pre = new long[2 * n + 1];
            for (int i = 0; i < 2 * n; ++i) pre[i + 1] = pre[i] + a[i % n];
            int l = 0, r = int.MaxValue;
            int[,] f = new int[2 * n + 1, 21];
            int[] ans = new int[n];
            while (l < r) {
                int mid = l + (r - l) / 2;
                if (check(mid)) l = mid + 1;
                else r = mid;
            }
            check(--l);
            output.AppendFormat("{0} {1}", l, ans.Where((a, i) => a > i + n).Count());
            Console.Write(output.ToString());

            bool check(int mid) {
                bool suc = false;
                int j = 0;
                for (int i = 0; i < 2 * n; ++i) {
                    while (j < 2 * n - 1 && pre[j + 1] - pre[i] < mid) ++j;
                    f[i, 0] = j + 1;
                }
                f[2 * n, 0] = 2 * n;
                for (j = 1; j <= 20; ++j) for (int i = 0; i <= 2 * n; ++i) f[i, j] = f[f[i, j - 1], j - 1];
                for (int i = 0; i < n; ++i) {
                    int p = i;
                    for (j = 20; j >= 0; --j) if ((k & (1 << j)) != 0) p = f[p, j];
                    if ((ans[i] = p) <= i + n) suc = true;
                }
                return suc;
            }
        }
    }
}