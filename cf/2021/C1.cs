using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateC1;

#if !PROBLEM
SolutionC1 a = new();
a.Solve();
#endif

namespace TemplateC1
{
    internal class SolutionC1
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
                int n = Read<int>(), m = Read<int>(), q = Read<int>();
                int[] a = ReadArray<int>(n), b = ReadArray<int>(m);
                int[] l = new int[n + 1];
                Array.Fill(l, m);
                for (int i = 0; i < m; ++i) {
                    if (l[b[i]] == m) l[b[i]] = i;
                }
                bool suc = true;
                for (int i = 1; i < n && suc; ++i) {
                    if (l[a[i - 1]] > l[a[i]]) suc = false;
                }
                output.AppendLine(suc ? "Ya" : "Tidak");
            }
            Console.Write(output.ToString());
        }
    }
}