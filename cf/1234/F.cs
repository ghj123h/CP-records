using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            string s = sr.ReadLine();
            int t = 20, M = 1 << t;
            int[] d = new int[M], mp = new int[20];
            for (int r = 0, l = 0; r < s.Length; ++r) {
                while (mp[s[r] - 'a'] > 0) --mp[s[l++] - 'a'];
                ++mp[s[r] - 'a'];
                int m = 0;
                for (int i = r; i >= l; --i) {
                    m |= 1 << s[i] - 'a';
                    d[m] = BitOperations.PopCount((uint)m);
                }
            }
            for (int m = 1; m < M; ++m) {
                for (int i = 0; i < 20; ++i) {
                    if ((m >> i & 1) != 0) {
                        d[m] = Math.Max(d[m], d[m ^ (1 << i)]);
                    }
                }
            }
            int ans = 0;
            for (int m = 0; m < M / 2; ++m) ans = Math.Max(ans, d[m] + d[M - m - 1]);
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}