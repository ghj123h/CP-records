using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateD;

#if !PROBLEM
SolutionD a = new();
a.Solve();
#endif

namespace TemplateD
{
    internal class SolutionD
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
                int[] p = ReadArray<int>(n);
                SparseTable st = new(p, Math.Min);
                int ans = 0;
                bool suc = true;
                for (int i = 2; i <= n; i <<= 1) {
                    for (int j = 0; j < n; j += i) {
                        int l = st.Query(j, j + i / 2 - 1), r = st.Query(j + i / 2, j + i - 1);
                        if (Math.Abs(l - r) != i / 2) {
                            suc = false;
                            break;
                        } else if (l > r) ++ans;
                    }
                }
                if (suc) output.Append(ans).AppendLine();
                else output.AppendLine("-1");
            }
            Console.Write(output.ToString());
        }
    }

    public class SparseTable {
        private int[,] st;
        private int[] logn;
        private Func<int, int, int> func;
        public SparseTable(int[] nums, Func<int, int, int> func) {
            this.func = func;
            int n = nums.Length;
            logn = new int[n + 2]; logn[1] = 0; logn[2] = 1;
            for (int i = 3; i <= n; ++i) logn[i] = logn[i / 2] + 1;
            st = new int[n, 21];
            for (int i = 0; i < n; ++i) st[i, 0] = nums[i];
            for (int j = 1; j <= 20; ++j) {
                for (int i = 0; i + (1 << j) - 1 < n; ++i) st[i, j] = func(st[i, j - 1], st[i + (1 << j - 1), j - 1]);
            }
        }

        public int Query(int l, int r) {
            int lg = logn[r - l + 1];
            return func(st[l, lg], st[r - (1 << lg) + 1, lg]);
        }
    }
}