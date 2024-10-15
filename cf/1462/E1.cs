using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE1;

#if !PROBLEM
SolutionE1 a = new();
a.Solve();
#endif

namespace TemplateE1
{
    internal class SolutionE1
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
                // int m = 3, k = 2;
                int[] a = ReadArray<int>(n);
                int[] mp = new int[n + 1];
                foreach (var x in a) ++mp[x];
                int[] pre = new int[n + 4];
                for (int i = 1; i <= n + 2; ++i) pre[i + 1] = pre[i] + (i <= n ? mp[i] : 0);
                long ans = 0;
                for (int i = 1; i <= n; ++i) {
                    int v = pre[i + 3] - pre[i], u = v - mp[i];
                    ans += 1L * v * (v - 1) * (v - 2) / 6 - 1L * u * (u - 1) * (u - 2) / 6;
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}