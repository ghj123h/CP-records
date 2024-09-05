﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Template;

#if !PROBLEM
SolutionA a = new();
a.Solve();
#endif

namespace Template
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
            int a = Read<int>(), b = Read<int>();
            if (a == b) output.Append(1);
            else if ((a - b) % 2 == 0) output.Append(3);
            else output.Append(2);
            Console.WriteLine(output.ToString());
        }
    }
}