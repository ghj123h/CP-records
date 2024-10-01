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

        public async void Solve()
        {
            StringBuilder output = new();
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>();
                int[][][] vals = new int[11][][];
                for (int i = 1; i <= 10; ++i) {
                    vals[i] = new int[i][];
                    for (int j = 0; j < i; ++j) {
                        vals[i][j] = new int[(n - j) / i + 1];
                    }
                }
                for (int i = 0; i < m; ++i) {
                    int a = Read<int>(), d = Read<int>(), k = Read<int>();
                    vals[d][a % d][a / d]++;
                    vals[d][a % d][a / d + k]--;
                }
                DisjointSet dsu = new(n + 1);
                for (int i = 1; i <= 10; ++i) {
                    for (int j = 0; j < i; ++j) {
                        for (int k = 0; k < vals[i][j].Length; ++k) {
                            if (k > 0) vals[i][j][k] += vals[i][j][k - 1];
                            if (vals[i][j][k] > 0) dsu.Merge(k * i + j, (k + 1) * i + j);
                        }
                    }
                }
                output.Append(dsu.Count - 1).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }

    public class DisjointSet {
        private int n;
        private int[] fa;
        private void Update() {
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
        public int Size(int x) { Update(); return fa.Where(t => t == fa[x]).Count(); }
    }
}