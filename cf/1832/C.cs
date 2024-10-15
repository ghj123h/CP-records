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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                int ans = 1;
                bool r = true;
                int i;
                for (i = 1; i < n; ++i) {
                    if (a[i] != a[i - 1]) {
                        r = a[i] > a[i - 1];
                        ++ans;
                        break;
                    }
                }
                for (; i < n; ++i) {
                    if (r && a[i] < a[i-1]) {
                        r = !r;
                        ++ans;
                    } else if (!r && a[i] > a[i-1]) {
                        r = !r;
                        ++ans;
                    }
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}