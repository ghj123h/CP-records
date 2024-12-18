﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB1;

#if !PROBLEM
SolutionB1 a = new();
a.Solve();
#endif

namespace TemplateB1
{
    internal class SolutionB1
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
            Queue<int> q = new();
            SortedSet<int> st = new();
            int n = Read<int>(), k = Read<int>();
            for (int i = 0; i < n; ++i) {
                int v = Read<int>();
                if (!st.Contains(v)) {
                    if (st.Count == k) {
                        st.Remove(q.Dequeue());
                    }
                    q.Enqueue(v);
                    st.Add(v);
                }
            }
            output.Append(st.Count).AppendLine();
            output.AppendJoin(' ', q.Reverse()).AppendLine();
            Console.Write(output.ToString());
        }
    }
}