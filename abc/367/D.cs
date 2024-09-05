using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionD a = new();
a.Solve();
#endif

namespace Template
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
            // int T = Read<int>();
            int T = 1;
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>();
                int[] a = ReadArray<int>(n);
                a = a.Concat(a).ToArray();
                long[] pre = new long[2 * n + 1];
                for (int i = 0; i < 2 * n; ++i) pre[i + 1] = pre[i] + a[i];
                int[] mp = new int[m];
                for (int i = 0; i < n - 1; ++i) mp[pre[i] % m]++;
                long ans = 0;
                for (int r = n - 1; r < 2 * n - 1; ++r)
                {
                    ans += mp[pre[r] % m];
                    ++mp[pre[r] % m];
                    --mp[pre[r - n + 1] % m];
                }
                output.Append(ans);
            }
            Console.WriteLine(output.ToString());
        }
    }
}