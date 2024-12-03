using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB;

#if !PROBLEM
SolutionB a = new();
a.Solve();
#endif

namespace TemplateB
{
    internal class SolutionB
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
                int[] a = ReadArray<int>(n), b = ReadArray<int>(n);
                int[] c = new int[n];

                for (int i = 0; i < n; ++i) {

                }
            }
            Console.Write(output.ToString());
        }
    }

    public class LazySegTree {
        private int[] tree, lazy;
        private int n;

        private void Build(int[] arr, int v, int l, int r) {
            if (l == r) tree[v] = arr[l];
            else {
                int m = l + (r - l) / 2;
                Build(arr, v * 2 + 1, l, m);
                Build(arr, v * 2 + 2, m + 1, r);
                tree[v] = Math.Max(tree[v * 2 + 1], tree[v * 2 + 2]);
            }
        }
        private void Push(int v) {
            tree[v * 2 + 1] = Math.Max(tree[v * 2 + 1], v); lazy[v * 2 + 1] += lazy[v];
            tree[v * 2 + 2] = Math.Max(tree[v * 2 + 2], v); lazy[v * 2 + 2] += lazy[v];
            lazy[v] = 0;
        }
        private void Update(int v, int L, int R, int l, int r, int add) {
            if (l >= L && r <= R) {
                tree[v] += add;
                lazy[v] += add;
            } else if (L <= R) {
                Push(v);
                int m = l + (r - l) / 2;
                Update(v * 2 + 1, L, Math.Min(R, m), l, m, add);
                Update(v * 2 + 2, Math.Max(L, m + 1), R, m + 1, r, add);
                tree[v] = Math.Max(tree[v * 2 + 1], tree[v * 2 + 2]);
            }
        }
        private int Query(int v, int L, int R, int l, int r) {
            if (L > R) return -0x3f3f3f3f;
            else if (l >= L && r <= R) return tree[v];
            Push(v);
            int m = l + (r - l) / 2;
            int left = Query(v * 2 + 1, L, Math.Min(R, m), l, m);
            int right = Query(v * 2 + 2, Math.Max(L, m + 1), R, m + 1, r);
            return Math.Max(left, right);
        }

        public LazySegTree(int[] arr) {
            n = arr.Length;
            tree = new int[n * 4]; lazy = new int[n * 4];
            Build(arr, 0, 0, n - 1);
        }
        public void Update(int L, int R, int add) => Update(0, L, R, 0, n - 1, add);
        public int Query(int L, int R) => Query(0, L, R, 0, n - 1);
    }

}