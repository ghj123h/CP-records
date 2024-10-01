﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateD1;

#if !PROBLEM
SolutionD1 a = new();
a.Solve();
#endif

namespace TemplateD1
{
    internal class SolutionD1
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
                int[] a = ReadArray<int>(n);
                int min = a.Min();
                for (int i = 0; i < n; ++i) a[i] -= min;
                int ans = 0;
                for (int i = 0; i < n; ++i) ans = Gcd(ans, a[i]);
                output.Append(ans == 0 ? -1 : ans).AppendLine();
            }
            Console.Write(output.ToString());
        }

        static int Gcd(int a, int b) => a == 0 ? b : (b == 0 ? a : Gcd(b, a % b));
    }
}