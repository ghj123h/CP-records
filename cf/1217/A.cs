﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TemplateA;

#if !PROBLEM
SolutionA a = new();
a.Solve();
#endif

namespace TemplateA
{
    internal class SolutionA
    {
        private readonly StreamReader sr = new(Console.OpenStandardInput());
        private T Read<T>()
            where T: struct, IConvertible
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
                int a = Read<int>(), b = Read<int>(), c = Read<int>();
                // x + a > c - x + b
                // 2x > c + b - a, x > (c+b-a)/2
                int v = c + b - a;
                if (v < 0) v = -1;
                else v /= 2;
                output.Append(Math.Max(0, c - v)).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}