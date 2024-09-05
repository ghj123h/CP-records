using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace Template
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
            int h = Read<int>(), w = Read<int>(), n = Read<int>();
            (int r, int c)[] pt = new (int, int)[n];
            for (int i = 0; i < n; ++i)
            {
                int u = Read<int>(), v = Read<int>();
                pt[i] = (u, v);
            }
            Array.Sort(pt);
            int[] d = new int[n];
            SimpleSegTree seg = new SimpleSegTree(new int[w + 1]);
            Array.Sort(pt);
            int max = 0;
            for (int i = 0; i < n; ++i)
            {
                (d[i], _) = seg.Query(0, pt[i].c);
                seg.Update(pt[i].c, ++d[i]);
                max = Math.Max(max, d[i]);
            }
            output.Append(max).AppendLine();
            int r = 1, c = 1;
            foreach (var (rr, cc) in gen().Reverse())
            {
                output.AppendJoin("", Enumerable.Repeat('D', rr - r)).AppendJoin("", Enumerable.Repeat('R', cc - c));
                (r, c) = (rr, cc);
            }
            Console.WriteLine(output.ToString()); ;

            IEnumerable<(int, int)> gen()
            {
                yield return (h, w);
                int i = n, c = max;
                while (d[--i] != c) ;
                int j = i;
                yield return pt[j];
                while (--i >= 0)
                {
                    if (d[i] == c - 1 && pt[i].c <= pt[j].c)
                    {
                        yield return pt[j = i];
                        if (--c == 1) break;
                    }
                }
            }
        }
    }

    public class SimpleSegTree
    {
        private int[] tree, idx;
        private int n;

        private void Build(int[] arr, int v, int l, int r)
        {
            if (l == r)
            {
                tree[v] = arr[l];
                idx[v] = l;
            }
            else
            {
                int m = l + (r - l) / 2;
                Build(arr, v * 2 + 1, l, m);
                Build(arr, v * 2 + 2, m + 1, r);
                if (tree[v * 2 + 1] < tree[v * 2 + 2])
                {
                    tree[v] = tree[v * 2 + 2];
                    idx[v] = idx[v * 2 + 2];
                } else
                {
                    tree[v] = tree[v * 2 + 1];
                    idx[v] = idx[v * 2 + 1];
                }
            }
        }
        private void Update(int v, int i, int l, int r, int value)
        {
            if (l == r && i == l) tree[v] = value;
            else
            {
                int m = l + (r - l) / 2;
                if (i <= m) Update(v * 2 + 1, i, l, m, value);
                else Update(v * 2 + 2, i, m + 1, r, value);
                if (tree[v * 2 + 1] < tree[v * 2 + 2])
                {
                    tree[v] = tree[v * 2 + 2];
                    idx[v] = idx[v * 2 + 2];
                }
                else
                {
                    tree[v] = tree[v * 2 + 1];
                    idx[v] = idx[v * 2 + 1];
                }
            }
        }
        private (int, int) Query(int v, int L, int R, int l, int r)
        {
            if (L > R) return (0, -1);
            else if (l == L && r == R) return (tree[v], idx[v]);
            int m = l + (r - l) / 2;
            var (left, li) = Query(v * 2 + 1, L, Math.Min(R, m), l, m);
            var (right, ri) = Query(v * 2 + 2, Math.Max(L, m + 1), R, m + 1, r);
            if (left < right) return (right, ri);
            else return (left, li);
        }

        public SimpleSegTree(int[] arr)
        {
            n = arr.Length;
            tree = new int[n * 4];
            idx = new int[n * 4];
            Build(arr, 0, 0, n - 1);
        }
        public void Update(int i, int add) => Update(0, i, 0, n - 1, add);
        public (int, int) Query(int L, int R) => Query(0, L, R, 0, n - 1);
    }
}