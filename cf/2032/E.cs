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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                long[,] d = new long[2, n + 2];
                int mx = a.Max();
                for (int i = 0; i < n; ++i) {
                    d[i % 2, i / 2] += mx - a[i];
                    d[i % 2, i / 2 + n / 2 + 1] -= mx - a[i];
                }
                long[] ans = new long[n];
                for (int j = 0; j < 2; ++j) {
                    for (int i = 0; i < n + 2; ++i) {
                        if (i > 0) d[j, i] += d[j, i - 1]; // time of +1 +1 on each coord
                        ans[(i * 2 + j) % n] += d[j, i];
                    }
                }
                d.AsSpan().Fill(0);
                for (int i = 0; i < n; ++i) {
                    d[i % 2, i / 2] += ans[i];
                    d[i % 2, i / 2 + n / 2 + 1] -= ans[i];
                }
                Array.Fill(ans, 0);
                for (int j = 0; j < 2; ++j) {
                    for (int i = 0; i < n + 2; ++i) {
                        if (i > 0) d[j, i] += d[j, i - 1]; // time of +1 +1 on each coord
                        ans[(i * 2 + j + 1) % n] += d[j, i];
                    }
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