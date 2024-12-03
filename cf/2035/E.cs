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
                long x = Read<long>(), y = Read<long>(), z = Read<long>(), k = Read<long>();
                long d = 0;
                long ans = long.MaxValue, pre = 0;
                while (z > 0) {
                    long l = d + 1, r = d + k;
                    long u = (long)Math.Floor(Math.Sqrt(z - 1) + 1e-3);
                    if (u >= r) {
                        Cal1(l, r);
                    } else if (u <= d) {
                        Cal2(l, r);
                    } else {
                        Cal1(l, u);
                        Cal2(u + 1, r);
                    }
                    pre += k * x + y;
                    z -= r;
                    d = r;

                    void Cal1(long l, long r) {
                        for (long i = l; i <= r; ++i) {
                            ans = Math.Min(ans, pre + (z + i - 1) / i * y + (i - d) * x);
                        }
                    }

                    void Cal2(long l, long r) {
                        long a = (z + r - 1) / r, b = (z + l - 1) / l;
                        long R = b == 1 ? r : (z - 1) / (b - 1), L = l;
                        for (long i = b; i >= a; --i) {
                            R = i == 1 ? r : (z - 1) / (i - 1);
                            R = Math.Min(r, R);
                            ans = Math.Min(ans, pre + (L - d) * x + i * y);
                            L = R + 1;
                        }
                    }
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}