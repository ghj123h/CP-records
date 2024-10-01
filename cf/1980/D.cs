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
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                int[] b = new int[--n];
                for (int i = 0; i < n; ++i) b[i] = Gcd(a[i], a[i + 1]);
                int cnt = 0;
                for (int i = 1; i < n; ++i) if (b[i] >= b[i - 1]) ++cnt;
                bool suc = false;
                suc = cnt == n - 1 || (cnt == n - 2 && (b[0] > b[1] || b[^2] > b[^1]));
                for (int i = 1; i < n && !suc; ++i) {
                    int u = cnt;
                    if (b[i] >= b[i - 1]) --u;
                    if (i > 1 && b[i - 1] >= b[i - 2]) --u;
                    if (i < n - 1 && b[i] <= b[i + 1]) --u;
                    int c = Gcd(a[i - 1], a[i + 1]);
                    if (i > 1 && c >= b[i - 2]) ++u;
                    if (i < n - 1 && c <= b[i + 1]) ++u;
                    if (u == n - 2) suc = true;
                }
                output.AppendLine(suc ? "Yes" : "No");
            }
            Console.Write(output.ToString());
        }

        public static int Gcd(int a, int b) => b == 0 ? a : Gcd(b, a % b);
    }
}