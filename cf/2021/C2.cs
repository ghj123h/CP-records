using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TemplateC2;

#if !PROBLEM
SolutionC2 a = new();
a.Solve();
#endif

namespace TemplateC2
{
    internal class SolutionC2
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
            while (T-- > 0) {
                int n = Read<int>(), m = Read<int>(), q = Read<int>();
                int[] a = new int[n + 2], b = new int[m + 1];
                a[0] = 0; a[n + 1] = n + 1;
                for (int i = 1; i <= n; ++i) a[i] = Read<int>();
                for (int i = 1; i <= m; ++i) b[i] = Read<int>();
                int[] mp = new int[n + 1];
                for (int i = 1; i <= n; ++i) mp[a[i]] = i;
                SortedSet<int>[] st = new SortedSet<int>[n + 2];
                st[0] = new SortedSet<int> { 0 };
                for (int i = 1; i <= n + 1; ++i) st[i] = new SortedSet<int> { m + 1 };
                for (int i = 1; i <= m; ++i) st[b[i]].Add(i);
                int tot = 0;
                for (int i = 1; i <= n + 1; ++i) if (st[a[i - 1]].Min <= st[a[i]].Min) ++tot;
                output.AppendLine(tot == n + 1 ? "Ya" : "Tidak");
                while (q-- > 0) {
                    int s = Read<int>(), t = Read<int>();
                    int i = mp[b[s]], j = mp[t];
                    if (st[a[i - 1]].Min <= st[a[i]].Min) --tot;
                    if (st[a[i]].Min <= st[a[i + 1]].Min) --tot;
                    st[b[s]].Remove(s);
                    if (st[a[i - 1]].Min <= st[a[i]].Min) ++tot;
                    if (st[a[i]].Min <= st[a[i + 1]].Min) ++tot;

                    b[s] = t;
                    if (st[a[j - 1]].Min <= st[a[j]].Min) --tot;
                    if (st[a[j]].Min <= st[a[j + 1]].Min) --tot;
                    st[t].Add(s);
                    if (st[a[j - 1]].Min <= st[a[j]].Min) ++tot;
                    if (st[a[j]].Min <= st[a[j + 1]].Min) ++tot;
                    output.AppendLine(tot == n + 1 ? "Ya" : "Tidak");
                }
            }
            Console.Write(output.ToString());
        }
    }
}