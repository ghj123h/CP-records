using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TemplateG;

#if !PROBLEM
SolutionG a = new();
a.Solve();
#endif

namespace TemplateG {
    internal class SolutionG {
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
            T[] arr = new T[n + startIndex + 1];
            for (int i = startIndex; i < n + startIndex; ++i) arr[i] = Read<T>();
            return arr;
        }

        public void Solve() {
            StringBuilder output = new();
            int n = Read<int>();
            int[] p = ReadArray<int>(n, 1), a = ReadArray<int>(n, 1);
            List<int>[] f = new List<int>[n + 1];
            for (int i = 1; i <= n; ++i) f[i] = new();
            for (int i = 1; i <= n; ++i) for (int j = 1; j <= n; j += i) f[j].Add(i);
            int[] p1 = new int[n + 2], r = new int[n + 1], use = new int[n + 1], ans = new int[n + 1];
            bool[] vis = new bool[n + 1];
            p1[n + 1] = a[n + 1] = n + 1;
            for (int i = 1; i <= n; ++i) {
                int j = i;
                if (vis[i]) continue;
                int t = n + 1, cnt = 0;
                while (p[j] != i) {
                    vis[p1[cnt++] = j] = true;
                    j = p[j];
                }
                vis[p1[cnt++] = j] = true;
                j = p[j];
                int u = 0;
                Array.Fill(use, 0, 0, cnt);
                foreach (var l in f[cnt]) {
                    if (r[l] != r[0]) {
                        ++u;
                        for (int k = r[l]; k < cnt; k += l) ++use[k];
                    }
                }
                for (j = 0; j < cnt; ++j) if (use[j] == u) if (a[p1[j]] < a[p1[t]]) t = j;
                foreach (var l in f[cnt]) r[l] = t % l;
                t = p1[t];
                j = i;
                while (cnt-- > 0) {
                    ans[j] = a[t];
                    j = p[j]; t = p[t];
                }
            }
            output.AppendJoin(' ', ans.Skip(1));
            Console.Write(output.ToString());
        }
    }
}