using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateC;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace TemplateC
{
    internal class SolutionC
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
            int n = Read<int>();
            int[] a = ReadArray<int>(n), b = ReadArray<int>(n - 1);
            Array.Sort(a); Array.Sort(b);
            bool pc = false;
            int ans = -1;
            for (int i = n - 1; i > 0; --i) {
                if (!pc) {
                    if (a[i] > b[i - 1]) {
                        ans = a[i];
                        pc = true;
                    }
                } else {
                    if (a[i] > b[i]) {
                        ans = -1;
                        break;
                    }
                }
            }
            if (!pc) ans = a[0];
            else if (a[0] > b[0]) ans = -1;
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}