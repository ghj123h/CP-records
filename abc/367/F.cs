using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace Template
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
            // int T = Read<int>();
            int T = 1;
            long mod = 1000000007;
            mod *= mod;
            while (T-- > 0)
            {
                int n = Read<int>(), q = Read<int>();
                int[] a = ReadArray<int>(n), b = ReadArray<int>(n);
                long[] hash = new long[n + 1];
                Random r = new();
                for (int i = 1; i <= n; ++i) hash[i] = r.NextInt64(0, mod);
                long[] prea = new long[n + 1], preb = new long[n + 1];
                long[] ca = new long[n + 1], cb = new long[n + 1];
                for (int i = 0; i < n; ++i)
                {
                    prea[i + 1] = (prea[i] + hash[a[i]]) % mod;
                    preb[i + 1] = (preb[i] + hash[b[i]]) % mod;
                    ca[i + 1] = ca[i] ^ hash[a[i]];
                    cb[i + 1] = cb[i] ^ hash[b[i]];
                }
                while (q-- > 0)
                {
                    int l1 = Read<int>(), r1 = Read<int>(), l2 = Read<int>(), r2 = Read<int>();
                    if (r1 - l1 == r2 - l2 && (ca[r1] ^ ca[l1 - 1]) == (cb[r2] ^ cb[l2 - 1])
                        && (prea[r1] - prea[l1 - 1] + mod) % mod == (preb[r2] - preb[l2 - 1] + mod) % mod) output.AppendLine("Yes");
                    else output.AppendLine("No");
                }
            }
            Console.Write(output.ToString());
        }
    }
}