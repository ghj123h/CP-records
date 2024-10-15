using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                sr.ReadLine();
                string[] s = new string[2];
                for (int i = 0; i < 2; ++i) s[i] = sr.ReadLine();
                int[] d = new int[n + 1];
                d[0] = 0;
                for (int i = 0; i < n; ++i) {
                    switch (i % 3) {
                        case 0:
                            d[i + 1] = Math.Max(d[i + 1], d[i] + Vote(s[0][i], s[1][i], s[1][i + 1]));
                            if (i >= 2) d[i + 1] = Math.Max(d[i + 1], d[i - 2] + Vote(s[0][i], s[0][i - 1], s[0][i - 2]) + Vote(s[1][i + 1], s[1][i], s[1][i - 1]));
                            break;
                        case 1:
                            d[i + 1] = Math.Max(d[i + 1], d[i - 1] + Vote(s[0][i], s[0][i - 1], s[1][i - 1]));
                            if (i >= 2) d[i + 1] = Math.Max(d[i + 1], d[i - 2] + Vote(s[0][i], s[0][i - 1], s[0][i - 2]) + Vote(s[1][i - 1], s[1][i - 2], s[1][i - 3]));
                            break;
                        case 2:
                            d[i + 1] = Math.Max(d[i + 1], d[i] + Vote(s[0][i], s[1][i], s[1][i - 1]));
                            d[i + 1] = Math.Max(d[i + 1], d[i - 1] + Vote(s[0][i], s[1][i], s[0][i - 1]));
                            if (i >= 2) d[i + 1] = Math.Max(d[i + 1], d[i - 2] + Vote(s[0][i], s[0][i - 1], s[0][i - 2]) + Vote(s[1][i], s[1][i - 1], s[1][i - 2]));
                            break;
                    }
                }
                output.Append(d[n]).AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static int Vote(char a, char b, char c) {
            if ((a == 'A' && b == 'A') || (a == 'A' && c == 'A') || (b == 'A' && c == 'A')) return 1;
            else return 0;
        }
    }
}