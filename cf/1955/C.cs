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
                long k = Read<long>();
                int[] b = ReadArray<int>(n);
                int l = 0, r = n - 1;
                while (l < r) {
                    if (b[l] <= b[r]) {
                        int t = b[l] * 2 - 1;
                        if (k >= t) {
                            k -= t;
                            if (k > 0) {
                                b[r] -= b[l++];
                                --k;
                                if (b[r] == 0) --r;
                            } else {
                                ++l;
                                break;
                            }
                        } else {
                            k = 0;
                            break;
                        }
                    } else {
                        int t = b[r] * 2;
                        if (k >= t) {
                            k -= t;
                            b[l] -= b[r--];
                        } else {
                            k = 0;
                            break;
                        }
                    }
                }
                if (l == r && k >= b[l]) ++l;
                output.Append(n - (r - l + 1)).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}