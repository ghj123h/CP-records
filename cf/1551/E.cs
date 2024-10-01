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
                int n = Read<int>(), k = Read<int>();
                int[] a = ReadArray<int>(n);
                for (int i = 0; i < n; ++i) a[i] = i + 1 - a[i];
                int[,] d = new int[n + 1, n - k + 1];
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j <= n - k; ++j) d[i + 1, j] = d[i, j];
                    if (a[i] <= n - k) {
                        for (int j = 0; j <= i && j <= a[i]; ++j) {
                            d[i + 1, a[i]] = Math.Max(d[i + 1, a[i]], d[i - j, a[i] - j] + 1);
                        }
                    }
                }
                bool suc = false;
                for (int j = 0; j <= n - k; ++j) {
                    if (d[n, j] >= k) {
                        output.Append(j).AppendLine();
                        suc = true; break;
                    }
                }
                if (!suc) output.AppendLine("-1");
            }
            Console.Write(output.ToString());

            int UpperBound(IList<int> A, int u) {
                int L = 0, R = A.Count;
                while (L < R) {
                    int m = L + (R - L) / 2;
                    if (A[m] <= u) L = m + 1;
                    else R = m;
                }
                return L;
            }
        }
    }

    public class SimpleSegTree<TValue, TInfo> {
        private TInfo[] tree;
        private int n;
        private Func<TValue, int, TInfo> init;
        private TInfo eps;
        private Func<TInfo, TInfo, TInfo> merge;

        private void Build(TValue[] arr, int v, int l, int r) {
            if (l == r) tree[v] = init(arr[l], l);
            else {
                int m = l + (r - l) / 2;
                Build(arr, v * 2 + 1, l, m);
                Build(arr, v * 2 + 2, m + 1, r);
                tree[v] = merge(tree[v * 2 + 1], tree[v * 2 + 2]);
            }
        }
        private void Update(int v, int i, int l, int r, TValue value) {
            if (l == r && i == l) tree[v] = init(value, i);
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

        public SimpleSegTree(TValue[] arr, Func<TInfo, TInfo, TInfo> merge, Func<TValue, int, TInfo> init, TInfo eps) {
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