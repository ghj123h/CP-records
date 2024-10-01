using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TemplateF2;

#if !PROBLEM
SolutionF2 a = new();
a.Solve();
#endif

namespace TemplateF2
{
    internal class SolutionF2
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
                long n = Read<long>(), m = Read<long>(), k = Read<long>();
                (long r, long c)[] co = new (long r, long c)[k];
                for (int i = 0; i < k; ++i) co[i] = (Read<long>(), Read<long>());
                var idx = Enumerable.Range(0, (int)k).OrderBy(i => co[i].c).ThenByDescending(i => co[i].r).ToArray();
                long c = 1, h = n, hp = n;
                long area = 0;
                long[] ans = new long[k];
                int p = -1;
                for (int j = 0; j < k; ++j) {
                    int i = idx[j];
                    area += (co[i].c - c) * h;
                    if (p >= 0) ans[p] += (co[i].c - c) * (hp - h);
                    if (h > n - co[i].r) {
                        hp = h;
                        h = n - co[i].r;
                        p = i;
                    } else if (hp > n - co[i].r) {
                        hp = n - co[i].r;
                    }
                    c = co[i].c;
                }
                area += (m + 1 - c) * h;
                ans[p] += (m + 1 - c) * (hp - h);
                output.Append(area).AppendLine();
                output.AppendJoin(' ', ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}