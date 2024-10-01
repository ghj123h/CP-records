using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateG;

#if !PROBLEM
SolutionG a = new();
a.Solve();
#endif

namespace TemplateG
{
    internal class SolutionG
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

        public class Milk { public int d = 0, v = 0; }
        public void Solve()
        {
            StringBuilder output = new();
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>(), k = Read<int>();
                LinkedList<Milk> lst = new();
                int[] d = new int[n+1], a = new int[n];
                for (int i = 0; i < n; ++i) {
                    d[i] = Read<int>();
                    a[i] = Read<int>();
                }
                d[n] = d[^2] + k;
                int ans = 0;
                lst.AddLast(new Milk { d = d[0], v = a[0] });
                int cur = d[0], need = m;
                for (int i = 1; i <= n; ++i) {
                    if (cur != d[i - 1]) {
                        need = m;
                        cur = d[i - 1];
                    }
                    while (lst.Count > 0 && cur < d[i]) {
                        var fr = lst.Last.Value;
                        if (fr.d + k <= cur) break;
                        if (fr.v < need) {
                            need -= fr.v;
                            lst.RemoveLast();
                            continue;
                        }
                        ++cur;
                        fr.v -= need;
                        int rem = Math.Min(fr.d + k, d[i]) - cur;
                        int days = (fr.v + m - 1) / m;
                        if (days <= rem) {
                            cur += fr.v % m == 0 ? days : days - 1;
                            need = m - fr.v % m;
                            lst.RemoveLast();
                        } else {
                            fr.v -= rem * m;
                            cur += rem;
                            need = m;
                        }
                    }
                    ans += cur - d[i - 1];
                    while (lst.Count > 0 && lst.First.Value.d + k <= d[i]) lst.RemoveFirst();
                    if (i < n) lst.AddLast(new Milk { d = d[i], v = a[i] });
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}