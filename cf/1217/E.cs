using System;
using System.Collections.Generic;
using System.Globalization;
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
            int n = Read<int>(), m = Read<int>();
            int[] a = ReadArray<int>(n);
            int inf = 1999999999;
            SimpleSegTree<int, (int a, int b)>[] seg = new SimpleSegTree<int, (int a, int b)>[9];
            for (int i = 0, j = 1; i < 9; ++i, j *= 10) {
                int k = j;
                seg[i] = new(
                    a,
                    (l, r) => l.a <= r.a ? (l.a, Math.Min(r.a, l.b)) : (r.a, Math.Min(l.a, r.b)),
                    v => (v / k % 10 == 0 ? inf : v, inf),
                    (inf, inf)
                    );
            }
            while (m-- > 0) {
                int u = Read<int>(), v = Read<int>(), w = Read<int>();
                if (u == 1) {
                    for (int i = 0; i < 9; ++i) seg[i].Update(v - 1, w);
                } else {
                    int res = int.MaxValue;
                    for (int i = 0; i < 9; ++i) {
                        var q = seg[i].Query(v - 1, w - 1);
                        if (q.b < inf) res = Math.Min(res, q.a + q.b);
                    }
                    output.Append(res == int.MaxValue ? -1 : res).AppendLine();
                }
            }
            Console.Write(output.ToString());
        }
    }

    public class SimpleSegTree<TValue, TInfo> {
        private TInfo[] tree;
        private int n;
        private Func<TValue, TInfo> init;
        private TInfo eps;
        private Func<TInfo, TInfo, TInfo> merge;

        private void Build(TValue[] arr, int v, int l, int r) {
            if (l == r) tree[v] = init(arr[l]);
            else {
                int m = l + (r - l) / 2;
                Build(arr, v * 2 + 1, l, m);
                Build(arr, v * 2 + 2, m + 1, r);
                tree[v] = merge(tree[v * 2 + 1], tree[v * 2 + 2]);
            }
        }
        private void Update(int v, int i, int l, int r, TValue value) {
            if (l == r && i == l) tree[v] = init(value);
            else {
                int m = l + (r - l) / 2;
                if (i <= m) Update(v * 2 + 1, i, l, m, value);
                else Update(v * 2 + 2, i, m + 1, r, value);
                tree[v] = merge(tree[v * 2 + 1], tree[v * 2 + 2]);
            }
        }
        private TInfo Query(int v, int L, int R, int l, int r) {
            if (L > R) return eps;
            else if (l >= L && r <= R) return tree[v];
            int m = l + (r - l) / 2;
            TInfo left = Query(v * 2 + 1, L, Math.Min(R, m), l, m);
            TInfo right = Query(v * 2 + 2, Math.Max(L, m + 1), R, m + 1, r);
            return merge(left, right);
        }

        public SimpleSegTree(TValue[] arr, Func<TInfo, TInfo, TInfo> merge, Func<TValue, TInfo> init, TInfo eps) {
            n = arr.Length;
            tree = new TInfo[n * 4];
            this.merge = merge;
            this.init = init;
            this.eps = eps;
            Build(arr, 0, 0, n - 1);
        }
        public void Update(int i, TValue value) => Update(0, i, 0, n - 1, value);
        public TInfo Query(int L, int R) => Query(0, L, R, 0, n - 1);
    }
}