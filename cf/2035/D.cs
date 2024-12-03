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

        public void Solve()
        {
            StringBuilder output = new();
            int T = Read<int>();
            int mod = 1000000007;
            long[] pow2 = new long[30 * 200000 + 10];
            pow2[0] = 1;
            for (int i = 1; i < pow2.Length; ++i) pow2[i] = pow2[i - 1] * 2 % mod;
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                int[] t = new int[n], b = new int[n];
                Stack<int> st = new();
                long cur = 0;
                long[] ans = new long[n];
                for (int i = 0; i < n; ++i) {
                    b[i] = a[i];
                    while (b[i] % 2 == 0) {
                        ++t[i];
                        b[i] /= 2;
                    }
                    while (st.Count > 0 && cmp(st.Peek(), i)) {
                        int j = st.Pop();
                        t[i] += t[j];
                        cur += (mod - pow2[t[j]] * b[j] % mod) % mod;
                        cur += b[j];
                    }
                    cur += pow2[t[i]] * b[i] % mod;
                    cur %= mod;
                    st.Push(i);
                    ans[i] = cur;
                }
                output.AppendJoin(' ', ans).AppendLine();

                bool cmp(int i, int j) { // b[i] <= a[j]
                    if (t[j] >= 30) return true;
                    return b[i] <= (b[j] * 1L << t[j]);
                }
            }
            Console.Write(output.ToString());
        }
    }
}