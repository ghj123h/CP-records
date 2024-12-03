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
            int T = Read<int>(), maxn = 200000 + 10;
            Random r = new();
            long[] hash = new long[maxn], pre = new long[maxn];
            for (int i = 1; i < maxn; ++i) hash[i] = r.NextInt64();
            for (int i = 1; i < maxn; ++i) pre[i] = pre[i - 1] ^ hash[i];
            while (T-- > 0)
            {
                int n = Read<int>(), q = Read<int>();
                int[] a = ReadArray<int>(n);
                sr.ReadLine();
                string s = sr.ReadLine();
                for (int i = 0; i < n; ++i) a[i] = a[hash[i]];
                int[] p = new int[n + 1];
                for (int i = 0; i < n; ++i) p[i + 1] = p[i] ^ a[i];
                SortedDictionary<int, (int, char)> mp = new();
                int cnt = 1; char c = 'L';
                for (int i = 1; i < n; ++i) {
                    if (c == s[i]) ++cnt;
                    else {

                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}