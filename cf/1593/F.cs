using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TemplateF;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace TemplateF
{
    internal class SolutionF
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
                int n = Read<int>(), a = Read<int>(), b = Read<int>();
                sr.ReadLine();
                int[] A = sr.ReadLine().Select(c => c - '0').ToArray();
                int[,,,] d = new int[n + 1, n + 1, a, b];
                (int, int)[,,,] pre = new (int, int)[n + 1, n + 1, a, b];
                d[0, 0, 0, 0] = 1;
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j <= i; ++j) {
                        for (int aa = 0; aa < a; ++aa) {
                            for (int bb = 0; bb < b; ++bb) {
                                if (d[i, j, aa, bb] == 1) {
                                    int ap = (aa * 10 + A[i]) % a;
                                    int bp = (bb * 10 + A[i]) % b;
                                    d[i + 1, j + 1, ap, bb] = d[i + 1, j, aa, bp] = 1;
                                    pre[i + 1, j + 1, ap, bb] = (1, aa);
                                    pre[i + 1, j, aa, bp] = (0, bb);
                                }
                            }
                        }
                    }
                }
                StringBuilder ans = null;
                for (int dif = n % 2; dif < n && ans == null; dif += 2) {
                    int r = (n + dif) / 2, l = (n - dif) / 2;
                    gen(l, 0, 0);
                    gen(r, 0, 0);
                }
            end:
                if (ans == null) output.AppendLine("-1");
                else output.AppendLine(ans.ToString());

                void gen(int j, int aa, int bb) {
                    if (d[n, j, aa, bb] == 0) return;
                    ans = new();
                    ans.AppendJoin("", Enumerable.Repeat('B', n));
                    for (int i = n; i > 0; ) {
                        var (p, q) = pre[i--, j, aa, bb];
                        j -= p;
                        if (p > 0) { aa = q; ans[i] = 'R'; }
                        else bb = q;
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}