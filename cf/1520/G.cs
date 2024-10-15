using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateG;

#if !PROBLEM
SolutionG a = new();
a.Solve();
#endif

namespace TemplateG
{
    internal class SolutionG
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
            int n = Read<int>(), m = Read<int>(), w = Read<int>();
            int[,] G = new int[n, m];
            for (int i = 0; i < n; ++i) for (int j = 0; j < m; ++j) G[i, j] = Read<int>();
            int[,] d = new int[n, m];
            d.AsSpan().Fill(-1);
            long s = bfs(0, 0);
            d.AsSpan().Fill(-1);
            long t = bfs(n - 1, m - 1);
            long ans = long.MaxValue;
            if (s < long.MaxValue && t < long.MaxValue) ans = Math.Min(ans, s + t);
            if (d[0, 0] >= 0) ans = Math.Min(ans, 1L * d[0, 0] * w);
            output.Append(ans == long.MaxValue ? -1 : ans).AppendLine();
            Console.Write(output.ToString());

            IEnumerable<(int, int)> neighbors(int r, int c) {
                if (r > 0) yield return (r - 1, c);
                if (c > 0) yield return (r, c - 1);
                if (r < n - 1) yield return (r + 1, c);
                if (c < m - 1) yield return (r, c + 1);
            }

            long bfs(int r, int c) {
                Queue<(int, int)> q = new();
                q.Enqueue((r, c));
                d[r, c] = 0;
                long min = long.MaxValue;
                while (q.Count > 0) {
                    var (i, j) = q.Dequeue();
                    if (G[i, j] > 0) min = Math.Min(min, G[i, j] + 1L * d[i, j] * w);
                    foreach (var (ii, jj) in neighbors(i, j)) {
                        if (d[ii, jj] <= 0 && G[ii, jj] >= 0) {
                            d[ii, jj] = d[i, j] + 1;
                            q.Enqueue((ii, jj));
                        }
                    }
                }
                return min;
            }
        }
    }

    public static class ArrayExt {
        public static Span<T> AsSpan<T>(this T[,] array) => asSpan<T>(array);
        public static Span<T> AsSpan<T>(this T[,,] array) => asSpan<T>(array);
        static Span<T> asSpan<T>(Array array)
            => System.Runtime.InteropServices.MemoryMarshal.CreateSpan(
                ref System.Runtime.CompilerServices.Unsafe.As<byte, T>(
                    ref System.Runtime.InteropServices.MemoryMarshal.GetArrayDataReference(array)
                ), array.Length);
    }
}