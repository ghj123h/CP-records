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
                string[] rows = new string[2];
                sr.ReadLine();
                rows[0] = sr.ReadLine();
                rows[1] = sr.ReadLine();
                int x = 0, y = 0, d = 0;
                while (x <= 1 && y < n) {
                    if (rows[x][y] <= '2') {
                        if (d == 0) {
                            ++y;
                        } else {
                            break;
                        }
                    } else {
                        d ^= 1;
                        if (d == 0) ++y;
                        else x ^= 1;
                    }
                }
                output.AppendLine((x == 1 && y == n) ? "YES" : "NO");
            }
            Console.Write(output.ToString());
        }
    }
}