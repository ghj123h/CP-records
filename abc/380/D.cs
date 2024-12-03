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
            string s = sr.ReadLine();
            int q = Read<int>();
            long mx = 1000000;
            mx = mx * mx * mx;
            int n = s.Length;
            long r = n;
            while (r < mx) r *= 2;
            foreach (var k in ReadArray<long>(q)) {
                long rr = r, kk = k;
                bool rev = false;
                while (rr > n) {
                    if (kk > rr / 2) {
                        kk -= rr / 2;
                        rev = !rev;
                    }
                    rr >>= 1;
                }
                output.Append(rev ? flip(s[(int)(kk - 1)]) : s[(int)(kk - 1)]).Append(' ');
            }
            output.AppendLine();
            Console.Write(output.ToString());
        }

        public static char flip(char c) => char.IsLower(c) ? char.ToUpper(c) : char.ToLower(c);
    }
}