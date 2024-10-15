using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
            int n = Read<int>();
            int[] a = new int[n], b = new int[n];
            int sum = 0;
            for (int i = 0; i < n; ++i) {
                a[i] = Read<int>();
                b[i] = Read<int>();
                sum += b[i];
            }
            if (sum % 3 == 0) {
                sum /= 3;
                int[,,] d = new int[n + 1, sum + 1, sum + 1];
                d.AsSpan().Fill(0x3f3f3f3f);
                d[0, 0, 0] = 0;
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j <= sum; ++j) {
                        for (int k = 0; k <= sum; ++k) {
                            switch (a[i]) {
                                case 1:
                                    if (j >= b[i]) d[i + 1, j, k] = Math.Min(d[i + 1, j, k], d[i, j - b[i], k]);
                                    if (k >= b[i]) d[i + 1, j, k] = Math.Min(d[i + 1, j, k], d[i, j, k - b[i]] + 1);
                                    d[i + 1, j, k] = Math.Min(d[i + 1, j, k], d[i, j, k] + 1);
                                    break;
                                case 2:
                                    if (j >= b[i]) d[i + 1, j, k] = Math.Min(d[i + 1, j, k], d[i, j - b[i], k] + 1);
                                    if (k >= b[i]) d[i + 1, j, k] = Math.Min(d[i + 1, j, k], d[i, j, k - b[i]]);
                                    d[i + 1, j, k] = Math.Min(d[i + 1, j, k], d[i, j, k] + 1);
                                    break;
                                case 3:
                                    if (j >= b[i]) d[i + 1, j, k] = Math.Min(d[i + 1, j, k], d[i, j - b[i], k] + 1);
                                    if (k >= b[i]) d[i + 1, j, k] = Math.Min(d[i + 1, j, k], d[i, j, k - b[i]] + 1);
                                    d[i + 1, j, k] = Math.Min(d[i + 1, j, k], d[i, j, k]);
                                    break;
                            }
                        }
                    }
                }
                output.Append(d[n, sum, sum] == 0x3f3f3f3f ? -1 : d[n, sum, sum]).AppendLine();
            } else output.AppendLine("-1");
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