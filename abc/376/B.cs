using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB;

#if !PROBLEM
SolutionB a = new();
a.Solve();
#endif

namespace TemplateB
{
    internal class SolutionB
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
            int l = 1, r = 2, ans = 0;
            while (q-- > 0) {
                sr.ReadLine();
                char c = (char)sr.Read();
                int t = Read<int>();
                if (c == 'R') (l, r) = (r, l);
                if (l < r) {
                    if (t < r) ans += Math.Abs(l - t);
                    else ans += Math.Abs(n + l - t);
                } else {
                    if (t > r) ans += Math.Abs(l - t);
                    else ans += Math.Abs(n + t - l);
                }
                l = t;
                if (c == 'R') (l, r) = (r, l);
            }
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}