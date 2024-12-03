using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE;

#if !PROBLEM
SolutionE a = new();
a.Solve();
#endif

namespace TemplateE
{
    internal class SolutionE
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
            int n = Read<int>(), h = Read<int>(), l = Read<int>(), r = Read<int>();
            int[] a = ReadArray<int>(n);
            int[,] d = new int[n + 1, h];
            d.AsSpan().Fill(-0x3f3f3f3f);
            d[0, 0] = 0;
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < h; ++j) {
                    for (int k = 0; k < 2; ++k) {
                        int t = (j + a[i] - k) % h, s = Convert.ToInt32(t >= l && t <= r);
                        d[i + 1, t] = Math.Max(d[i + 1, t], d[i, j] + s);
                    }
                }
            }
            int ans = 0;
            for (int j = 0; j < h; ++j) {
                ans = Math.Max(ans, d[n, j]);
            }
            Console.WriteLine(ans);
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