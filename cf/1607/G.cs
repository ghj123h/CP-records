﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateG;

#if !PROBLEM
SolutionG a = new();
a.Solve();
#endif

namespace TemplateG
{
    internal class SolutionG
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
            while (T-- > 0) {
                int n = Read<int>(), m = Read<int>();
                (int l, int r)[] vals = new (int, int)[n];
                long sum = 0;
                for (int i = 0; i < n; ++i) {
                    int x = Read<int>(), y = Read<int>();
                    if (x >= m && y >= m) vals[i] = (-m, m);
                    else if (x >= m) vals[i] = (m - 2 * y, m);
                    else if (y >= m) vals[i] = (-m, 2 * x - m);
                    else vals[i] = (m - 2 * y, 2 * x - m);
                    sum += x - y;
                }
                int[] diff = vals.Select(v => v.l).ToArray();
                long bal = sum - diff.Sum(d => 1L * d);
                if (bal > 0) {
                    for (int i = 0; i < n; ++i) {
                        var (l, r) = vals[i];
                        if (r - l > bal) {
                            int t = (int)(bal / 2 * 2);
                            diff[i] += t;
                            bal -= t;
                            break;
                        } else {
                            diff[i] = r;
                            bal -= r - l;
                        }
                    }
                }
                output.Append(Math.Abs(bal)).AppendLine();
                for (int i = 0; i < n; ++i) output.AppendFormat("{0} {1}\n", (diff[i] + m) / 2, (m - diff[i]) / 2);
            }
            Console.Write(output.ToString());
        }
    }
}