using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TemplateC2;

#if !PROBLEM
SolutionC2 a = new();
a.Solve();
#endif

namespace TemplateC2
{
    internal class SolutionC2
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

        public void Solve() {
            StringBuilder output = new();
            int T = Read<int>();
            while (T-- > 0) {
                long x = Read<long>();
                long m = Read<long>();
                int m1 = (1 << 32 - BitOperations.LeadingZeroCount((uint)x)) - 1;
                int m2 = (int)Math.Min(m, m1 / 2);
                long ans = 0;
                for (int y = 1; y <= m2; ++y) {
                    for (int xor = y * 2; xor <= m1; xor += y) {
                        int xx = y ^ xor;
                        if (xx == x && xor % x != 0) ++ans;
                    }
                }
                long L = 0;
                for (int k = 61; k >= 0; --k) {
                    L |= (x >> k & 1) << k;
                    if ((m >> k & 1) > 0) {
                        long R = L | ((1L << k) - 1);
                        ans += Query(L, R);
                        L ^= 1L << k;
                    } 
                }
                ans += Query(L, L);
                --ans;
                output.Append(ans).AppendLine();
                long Query(long l, long r) {
                    if (l == 0) return r / x + 1;
                    else return r / x - (l - 1) / x;
                }
            }
            Console.Write(output.ToString());
        }
            
    }
}