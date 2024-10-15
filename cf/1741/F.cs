using System;
using System.Collections.Generic;
using System.Linq;
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
            for (int t = 1; t <= T; ++t)
            {
                int n = Read<int>();
                (int l, int r, int c)[] vals = new (int l, int r, int c)[n];
                for (int i = 0; i < n; ++i) {
                    vals[i] = (Read<int>(), Read<int>(), Read<int>());
                }
                List<int> tmp = new();
                long[] ans = new long[n];
                Array.Fill(ans, 0x3f3f3f3f3f3f3f3f);
                Proc(vals); 
                output.AppendJoin(' ', ans).AppendLine();
                
                void Proc((int l, int r, int c)[] arr) {
                    long r1 = -0x3f3f3f3f3f3f3f3f, r2 = -0x3f3f3f3f3f3f3f3f;
                    int c1 = 0, c2 = 0;
                    foreach (var i in Enumerable.Range(0, n).OrderBy(i => arr[i])) {
                        // left
                        if (c1 != arr[i].c) {
                            ans[i] = Math.Min(ans[i], Math.Max(arr[i].l - r1, 0));
                        } else {
                            ans[i] = Math.Min(ans[i], Math.Max(arr[i].l - r2, 0));
                        }
                        if (arr[i].r > r1) {
                            if (c1 != arr[i].c) {
                                c2 = c1;
                                r2 = r1;
                                c1 = arr[i].c;
                            }
                            r1 = arr[i].r;
                        } else if (arr[i].r > r2 && c1 != arr[i].c) {
                            c2 = arr[i].c;
                            r2 = arr[i].r;
                        }
                        // right
                        if (tmp.Count > 0 && arr[tmp[^1]].c != arr[i].c) {
                            foreach (var j in tmp) {
                                int d = Math.Max(arr[i].l - arr[j].r, 0);
                                ans[j] = Math.Min(ans[j], d);
                            }
                            tmp.Clear();
                        }
                        tmp.Add(i);
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}