﻿using System;
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
            int T = Read<int>();
            int[] mpa = new int[1048576], mpb = new int[1048576];
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>(), k = Read<int>();
                int[] a = ReadArray<int>(n), b = ReadArray<int>(m);
                int tot = 0;
                foreach (var x in b) ++mpb[x];
                int r = m;
                for (int i = 0; i < r; ++i) {
                    if (++mpa[a[i]] <= mpb[a[i]]) ++tot;
                }
                int ans = tot >= k ? 1 : 0;
                while (r < n) {
                    int l = r - m;
                    if (++mpa[a[r]] <= mpb[a[r]]) ++tot;
                    if (mpa[a[l]]-- <= mpb[a[l]]) --tot;
                    if (tot >= k) ++ans;
                    ++r;
                }
                output.Append(ans).AppendLine();
                foreach (var x in a) mpa[x] = 0;
                foreach (var x in b) mpb[x] = 0;
            }
            Console.Write(output.ToString());
        }
    }
}