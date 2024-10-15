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
                long[] h = new long[n + 2];
                for (int i = 1; i <= n; ++i) h[i] = Read<long>();
                int[] l = new int[n + 1], r = new int[n + 1];
                Array.Fill(r, n + 1);
                Stack<int> st = new();
                for (int i = 1; i <= n; ++i) {
                    while (st.Count > 0 && h[st.Peek()] + st.Peek() > h[i] + i) r[st.Pop()] = i;
                    st.Push(i);
                }
                st.Clear();
                for (int i = n; i >= 1; --i) {
                    while (st.Count > 0 && h[st.Peek()] - st.Peek() > h[i] - i) l[st.Pop()] = i;
                    st.Push(i);
                }
                long[] left = new long[n + 2], right = new long[n + 2];
                for (int i = 1; i <= n; ++i) {
                    long w = i - l[i];
                    if (h[i] - w > 0) left[i] = left[i - w] + (h[i] + h[i] - w + 1) * w / 2;
                    else left[i] = h[i] * (h[i] + 1) / 2;
                }
                for (int i = n; i >= 1; --i) {
                    long w = r[i] - i;
                    if (h[i] - w > 0) right[i] = right[i + w] + (h[i] + h[i] - w + 1) * w / 2;
                    else right[i] = h[i] * (h[i] + 1) / 2;
                }
                long max = 0;
                for (int i = 1; i <= n; ++i) {
                    max = Math.Max(max, left[i] + right[i] - 2 * h[i]);
                }
                output.Append(h.Sum() - max).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}