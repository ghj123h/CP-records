using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TemplateF2;

#if !PROBLEM
SolutionF2 a = new();
a.Solve();
#endif

namespace TemplateF2
{
    internal class SolutionF2
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
            int n = Read<int>(), t = Read<int>();
            BITree fen = new(n + 1);
            int inf = 0x3f3f3f3f;
            fen.Add(1, -inf);
            while (t-- > 0) {
                int k = Read<int>();
                int l = 1, r = n + 1;
                while (l < r) {
                    int m = l + (r - l) / 2;
                    int q;
                    if ((q = fen.Query(m)) < 0) {
                        Console.WriteLine("? {0} {1}", 1, m);
                        Console.Out.Flush();
                        var f = q;
                        q = Read<int>();
                        fen.Add(m, q - f);
                        fen.Add(m + 1, f - q);
                    }
                    if (m - q >= k) r = m;
                    else l = m + 1;
                }
                Console.WriteLine("! {0}", l);
                Console.Out.Flush();
                fen.Add(l, 1);
            }
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

        public int Query(int i) {
            long res = 0;
            for (; i > 0; i -= lb(i)) res += C[i];
            return (int)res;
        }
    }
}