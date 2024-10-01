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
                int[] p = ReadArray<int>(n);
                int[] idx = new int[n + 1];
                for (int i = 0; i < n; ++i) idx[p[i]] = i;
                bool[] head = new bool[n];
                int j = n;
                for (int i = 1; i <= n; ++i) {
                    if (idx[i] < j) head[j = idx[i]] = true;
                }
                LinkedList<int> deq = new();
                for (int i = 0; i < n; ++i)
                    if (head[i]) deq.AddFirst(p[i]);
                    else deq.AddLast(p[i]);
                output.AppendJoin(' ', deq).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}