using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateF;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace TemplateF
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
            int n = Read<int>(), W = Read<int>();
            int[] w = new int[n], v = new int[n];
            for (int i = 0; i < n; ++i) {
                w[i] = Read<int>();
                v[i] = Read<int>();
            }
            Array.Sort(w, v);
            long[] C = new long[W + 1], d = new long[W + 1];
            PriorityQueue<int, int> q = new();

            int l = 0, r = 0;
            for (int i = 1; i <= W; ++i) {
                while (l < n && w[l] < i) ++l;
                while (r < n && w[r] <= i) ++r;
                if (l < r) {
                    Sub(v.AsSpan().Slice(l, r - l), i);
                    for (int j = W; j >= 0; --j) for (int k = 1; k <= j / i; ++k) d[j] = Math.Max(d[j], d[j - k * i] + C[k]);
                }
            }
            output.Append(d[W]).AppendLine();
            
            void Sub(Span<int> V, int w) {
                q.Clear();
                foreach (var v in V) {
                    q.Enqueue(v - 1, 1 - v);
                }
                for (int i = 1; i <= W / w; ++i) {
                    var v = q.Dequeue();
                    C[i] = C[i - 1] + v;
                    q.Enqueue(v - 2, 2 - v);
                }
            }
            Console.Write(output.ToString());
        }
    }
}