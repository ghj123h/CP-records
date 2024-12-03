using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TemplateC;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace TemplateC
{
    internal class SolutionC
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
            sr.ReadLine();
            while (T-- > 0)
            {
                string s = sr.ReadLine();
                int n = s.Length, m = 32 - BitOperations.LeadingZeroCount((uint)n);
                int p = -1;
                long ans = 0;
                for (int l = 0; l < n; ++l) {
                    if (s[l] == '1') {
                        int k = 0;
                        for (int r = l; r < l + m && r < n; ++r) {
                            k = k * 2 + s[r] - '0';
                            if (r - k + 1 > p) ++ans;
                            else break;
                        }
                        p = l;
                    }
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}