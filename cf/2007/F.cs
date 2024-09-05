using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                int[] d = new int[n - 1], dis = new int[n];
                for (int i = 0; i < n - 1; ++i) d[i] = Math.Abs(a[i + 1] - a[i]);
                dis[n - 1] = n - 1;
                for (int i = n - 2; i >= 0; --i) dis[i] = a[i] == a[i + 1] ? dis[i + 1] : i; // note that consecutive duplicates always satisfy
                SparseTable st = new(d, gcd);
                long ans = 1; // for last pos
                int l = 0, r = 0;
                for (int i = 0; i < n - 1; ++i)
                {
                    l = Math.Max(l, dis[i]); // move left to the final elem of the duplicates
                    r = Math.Max(r, l);
                    while (r < n - 1 && !BitOperations.IsPow2(st.Query(l, r))) ++r;
                    ans += n - 1 - r + dis[i] - i + 1;
                }
                output.Append(ans).AppendLine();
            }
            Console.WriteLine(output.ToString());
        }

        public static int gcd(int a, int b) => b == 0 ? a : gcd(b, a % b);
    }

    public class SparseTable
    {
        private int[,] st;
        private int[] logn;
        private Func<int, int, int> func;
        public SparseTable(int[] nums, Func<int, int, int> func)
        {
            this.func = func;
            int n = nums.Length;
            logn = new int[n + 1];
            if (n > 0) logn[1] = 0; 
            if (n > 1) logn[2] = 1;
            for (int i = 3; i <= n; ++i) logn[i] = logn[i / 2] + 1;
            st = new int[n, 21];
            for (int i = 0; i < n; ++i) st[i, 0] = nums[i];
            for (int j = 1; j <= 20; ++j)
            {
                for (int i = 0; i + (1 << j) - 1 < n; ++i) st[i, j] = func(st[i, j - 1], st[i + (1 << j - 1), j - 1]);
            }
        }

        public int Query(int l, int r)
        {
            int lg = logn[r - l + 1];
            return func(st[l, lg], st[r - (1 << lg) + 1, lg]);
        }
    }
}