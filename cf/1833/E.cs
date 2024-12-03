using System;
using System.Collections.Generic;
using System.Linq;
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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] d = new int[n + 1];
                DisjointSet dsu = new(n + 1);
                for (int i = 1; i <= n; ++i) {
                    int u = Read<int>();
                    dsu.Merge(i, u);
                    ++d[u];
                }
                int[] suc = new int[n + 1], cnt = new int[n + 1];
                dsu.Update();
                for (int i = 1; i <= n; ++i) {
                    ++cnt[dsu.Fa[i]];
                    if (d[i] == 0) ++suc[dsu.Fa[i]];
                }
                int min = 0, max = 0;
                for (int i = 1; i <= n; ++i) {
                    if (cnt[i] > 0) {
                        ++max;
                        if (suc[i] == 0 && cnt[i] > 2) ++min;
                    }
                }
                if (min < max) ++min;
                output.AppendFormat("{0} {1}\n", min, max);
            }
            Console.Write(output.ToString());
        }
    }

    public class DisjointSet {
        private int n;
        private int[] fa;
        internal void Update() {
            for (int i = 0; i < fa.Length; ++i) Find(i);
        }

        public DisjointSet(int n) {
            this.n = n;
            fa = Enumerable.Range(0, n).ToArray();
        }

        public int Find(int u) => fa[u] == u ? fa[u] : fa[u] = Find(fa[u]);
        public void Merge(int x, int y) {
            x = Find(x);
            y = Find(y);
            if (x != y) { fa[x] = y; --n; }
        }
        public int Count { get => n; }
        internal int[] Fa => fa;
        public int Size(int x) { Update(); return fa.Where(t => t == fa[x]).Count(); }
    }
}