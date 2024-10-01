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
            int n = Read<int>();
            int[] a = ReadArray<int>(n);
            var idx = Enumerable.Range(1, n).OrderByDescending(i => a[i-1]);
            int pre = int.MaxValue;
            BITree fen = new(n);
            List<int> toadd = new();
            int[] ans = new int[n + 1];

            foreach (var i in idx) {
                toadd.Add(i);
                int l = 1, r = i;
                int t = fen.Query(i);
                while (l < r) {
                    int m = l + (r - l) / 2;
                    int s = fen.Query(m);
                    if (s < t) l = m + 1;
                    else r = m;
                }
                ans[l - 1]++;
                ans[i - 1]--;

                if (pre > a[i-1]) {
                    foreach (var j in toadd) fen.Add(j, 1);
                    toadd.Clear();
                    pre = a[i-1];
                }
            }
            for (int i = 0; i < n; ++i) ans[i + 1] += ans[i];
            output.AppendJoin(' ', ans.Take(n)).AppendLine();
            Console.Write(output.ToString());
        }
    }

    public class BITree {
        private int n;
        private int[] C;
        private static int lb(int x) => x & -x;
        public BITree(int n) : this(Enumerable.Repeat(0, n).ToArray()) { }
        public BITree(int[] arr) { // note that the index of arr starts at 0, while C starts at 1
            this.n = arr.Length;
            C = new int[n + 1];
            for (int i = 1; i <= n; ++i) {
                C[i] += arr[i - 1];
                int j = i + lb(i);
                if (j <= n) C[j] += C[i];
            }
        }
        public void Add(int i, int value) { // note that this i starts at 1
            for (; i <= n; i += lb(i)) C[i] += value;
        }

        public int Query(int i) {
            int res = 0;
            for (; i > 0; i -= lb(i)) res += C[i];
            return res;
        }
    }
}