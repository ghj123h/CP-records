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
            int n = Read<int>(), q = Read<int>();
            sr.ReadLine();
            string s = sr.ReadLine();
            int[] o = new int[n + 1], t = new int[n + 1], sl = new int[n + 1];
            for (int i = 0; i < n; ++i) {
                o[i + 1] = o[i] + Convert.ToInt32(s[i] == '1');
                t[i + 1] = t[i] + Convert.ToInt32(s[i] == '2');
                sl[i + 1] = sl[i] + Convert.ToInt32(s[i] == '/');
            }
            while (q-- > 0) {
                int l = Read<int>(), r = Read<int>(); --l;
                int L = 0, R = r - l;
                while (L < R) {
                    int mid = (L + R) >> 1;
                    int l2 = LowerBound(o, o[l] + mid, l), r2 = UpperBound(t, t[r] - mid, r);
                    if (r2 > l2 && sl[r2] - sl[l2] > 0) L = mid + 1;
                    else R = mid;
                }
                output.Append(L > 0 ? 2 * L - 1 : 0).AppendLine();
            }
            Console.Write(output.ToString());

            int UpperBound(IList<int> A, int u, int r) {
                int L = 0, R = r;
                while (L < R) {
                    int m = L + (R - L) / 2;
                    if (A[m] <= u) L = m + 1;
                    else R = m;
                }
                return L;
            }

            int LowerBound(IList<int> A, int u, int l) {
                int L = l, R = A.Count;
                while (L < R) {
                    int m = L + (R - L) / 2;
                    if (A[m] < u) L = m + 1;
                    else R = m;
                }
                return L;
            }
        }
    }
}