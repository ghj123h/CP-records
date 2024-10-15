using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE1;

#if !PROBLEM
SolutionE1 a = new();
a.Solve();
#endif

namespace TemplateE1
{
    internal class SolutionE1
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
                int n = Read<int>(), m = Read<int>(), p = Read<int>();
                int[] s = ReadArray<int>(p);
                int[,] d = new int[n + 1, n + 1];
                d.AsSpan().Fill(int.MaxValue);
                for (int i = 1; i <= n; ++i) d[i, i] = 0;
                for (int i = 0; i < m; ++i) {
                    int u = Read<int>(), v = Read<int>(), w = Read<int>();
                    d[u, v] = d[v, u] = w;
                }
                for (int k = 1; k <= n; ++k) for (int i = 1; i <= n; ++i) for (int j = 1; j <= n; ++j)
                            d[i, j] = Math.Min(d[i, j], Math.Max(d[i, k], d[k, j]));
                long[] ans = new long[n], dis = new long[n + 1];
                var st = s.ToHashSet();
                Array.Fill(dis, int.MaxValue);
                for (int i = 0; i < p - 1; ++i) {
                    int u = 0;
                    ans[i] = long.MaxValue;
                    foreach (var v in st) {
                        long D = 0;
                        foreach (var vv in st) {
                            D += Math.Min(dis[vv], d[v, vv]);
                        }
                        if (ans[i] > D) {
                            ans[i] = D;
                            u = v;
                        }
                    }
                    st.Remove(u);
                    foreach (var v in st) dis[v] = Math.Min(dis[v], d[u, v]);
                }
                output.AppendJoin(' ', ans).AppendLine();
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