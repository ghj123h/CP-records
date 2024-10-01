using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE2;

#if !PROBLEM
SolutionE2 a = new();
a.Solve();
#endif

namespace TemplateE2
{
    internal class SolutionE2
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
                SortedDictionary<int, int> mp = new();
                for (int i = 0; i < n; ++i) mp.TryAdd(a[i], i);
                int u = 1;
                foreach (var k in mp.Keys.ToArray()) mp[k] = u++;
                BITree fen = new(u);
                long ans = 0;
                for (int i = 0; i < n; ++i) {
                    long l = fen.Query(mp[a[i]] - 1), r = fen.Query(u) - fen.Query(mp[a[i]]);
                    ans += Math.Min(l, r);
                    fen.Add(mp[a[i]], 1);
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }

    public class BITree {
        private int n;
        private long[] C;
        private static int lb(int x) => x & -x;
        public BITree(int n) : this(Enumerable.Repeat(0, n).ToArray()) { }
        public BITree(int[] arr) { // note that the index of arr starts at 0, while C starts at 1
            this.n = arr.Length;
            C = new long[n + 1];
            for (int i = 1; i <= n; ++i) {
                C[i] += arr[i - 1];
                int j = i + lb(i);
                if (j <= n) C[j] += C[i];
            }
        }
        public void Add(int i, int value) { // note that this i starts at 1
            for (; i <= n; i += lb(i)) C[i] += value;
        }

        public long Query(int i) {
            long res = 0;
            for (; i > 0; i -= lb(i)) res += C[i];
            return res;
        }
    }
}