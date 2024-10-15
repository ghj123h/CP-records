using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>();
                int[,] a = new int[n, m];
                for (int i = 0; i < n; ++i) for (int j = 0; j < m; ++j) a[i, j] = Read<int>();
                int ans = 1;
                Queue<(int, int)> q = new();
                bool[,] vis = new bool[n, m];
                for (int u = 1; u * u <= a[0,0]; ++u) {
                    if (a[0,0] % u == 0) {
                        if (feasible(a[0,0] / u)) {
                            ans = a[0, 0] / u;
                            break;
                        } else if (feasible(u)) {
                            ans = u;
                        }
                    }
                }
                output.Append(ans).AppendLine();

                IEnumerable<(int, int)> neighbors(int r, int c) {
                    if (r < n - 1) yield return (r + 1, c);
                    if (c < m - 1) yield return (r, c + 1);
                }

                bool feasible(int f) {
                    q.Clear();
                    vis.AsSpan().Fill(false);
                    q.Enqueue((0, 0));
                    vis[0, 0] = true;
                    while (q.Count > 0) {
                        var (r, c) = q.Dequeue();
                        foreach (var (i, j) in neighbors(r, c)) {
                            if (a[i, j] % f == 0 && !vis[i, j]) {
                                q.Enqueue((i, j));
                                vis[i, j] = true;
                            }
                        }
                    }
                    return vis[n - 1, m - 1];
                }
            }
            Console.Write(output.ToString());
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