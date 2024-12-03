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
            int T = Read<int>(), mod = 998244353;
            long[] t = new long[1048576];
            for (int i = 4; i < t.Length; ++i) t[i] = (t[i - 1] * i + i - 2) % mod;
            for (int i = 2; i < t.Length; ++i) t[i] = (t[i - 1] + t[i] + 1) % mod;
            while (T-- > 0)
            {
                output.Append(t[Read<int>()]).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}