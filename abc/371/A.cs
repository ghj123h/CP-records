using System;
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
            var s = sr.ReadLine();
            int[][] perm = { new int[] { 0, 1, 2 }, new int[] { 0, 2, 1 }, new int[] { 1, 0, 2 }, new int[] { 1, 2, 0 }, new int[] { 2, 1, 0 }, new int[] { 2, 0, 1 } };
            foreach (var p in perm)
            {
                if ($"{ch(p[0], p[1])} {ch(p[0], p[2])} {ch(p[1], p[2])}" == s)
                {
                    for (int i = 0; i < 3; ++i) if (p[i] == 1) output.Append((char)('A' + i));
                    break;
                }
            }
            Console.Write(output.ToString());

            char ch(int a, int b) => a < b ? '<' : '>';
        }
    }
}