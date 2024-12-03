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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>();
                int[] a = ReadArray<int>(n);
                Array.Sort(a);
                long prod = 1, ans = 0;
                Queue<int> win = new();
                for (int r = 0; r < n; ++r) {
                    int c = 1;
                    if (r > 0 && a[r] != a[r - 1] + 1) {
                        win.Clear();
                        prod = 1;
                    }
                    while (r < n - 1 && a[r + 1] == a[r]) {
                        ++r; ++c;
                    }
                    win.Enqueue(c);
                    prod = prod * c % Mod;
                    if (win.Count == m) {
                        ans = (ans + prod) % Mod;
                        prod = prod * Inv(win.Dequeue()) % Mod;
                    }
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
        public static readonly int Mod = 1000000007;
        public static long Inv(long a) {
            long u = 0, v = 1, m = Mod;
            while (a > 0) {
                long t = m / a;
                m -= t * a; (a, m) = (m, a);
                u -= t * v; (u, v) = (v, u);
            }
            return (u % Mod + Mod) % Mod;
        }
    }
}