using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateF1;

#if !PROBLEM
SolutionF1 a = new();
a.Solve();
#endif

namespace TemplateF1
{
    internal class SolutionF1
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
                long c = 1, h = n;
                long area = 0;
                int[] ans = new int[k];
                foreach (var i in idx) {
                    area += (co[i].c - c) * h;
                    if (h > n - co[i].r) {
                        h = n - co[i].r;
                        ans[i] = 1;
                    }
                    c = co[i].c;
                }
                area += (m + 1 - c) * h;
                output.Append(area).AppendLine();
                output.AppendJoin(' ', ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}