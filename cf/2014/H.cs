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

        public class Milk { public int d = 0, v = 0; }
        public void Solve()
        {
            StringBuilder output = new();
            int T = Read<int>();
            int[] mp = new int[1048576];
            while (T-- > 0)
            {
                int n = Read<int>(), q = Read<int>();
                int[] a = ReadArray<int>(n);
                (int l, int r)[] qu = new (int, int)[q];
                for (int i = 0; i < q; ++i) qu[i] = (Read<int>() - 1, Read<int>() - 1);
                int b = (int)Math.Sqrt(n);
                // if (b == 0) b = 1;
                int l = 0, r = -1;
                int meet = n;
                bool[] ans = new bool[q];
                foreach (var i in Enumerable.Range(0, q).OrderBy(j => qu[j].l / b).ThenBy(j => qu[j].r)) {
                    while (l > qu[i].l) Move(--l, 1);
                    while (r < qu[i].r) Move(++r, 1);
                    while (l < qu[i].l) Move(l++, -1);
                    while (r > qu[i].r) Move(r--, -1);
                    ans[i] = (r - l) % 2 == 1 && meet == n;
                }
                output.AppendJoin('\n', ans.Select(b => b ? "Yes" : "No")).AppendLine();
                foreach (var x in a) mp[x] = 0;

                void Move(int pos, int sign) {
                    if (sign > 0) mp[a[pos]]++;
                    else mp[a[pos]]--;
                    if (mp[a[pos]] % 2 == 0) ++meet;
                    else --meet;
                }
            }
            Console.Write(output.ToString());
        }
    }
}