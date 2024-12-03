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
            int T = Read<int>();
            while (T-- > 0)
            {
                int n = Read<int>(), a = Read<int>(), b = Read<int>();
                sr.ReadLine();
                string s = sr.ReadLine();
                bool suc = false;
                int x = 0, y = 0;
                while (!suc) {
                    foreach (var c in s) {
                        switch (c) {
                            case 'N':
                                ++y;
                                break;
                            case 'E':
                                ++x;
                                break;
                            case 'W':
                                --x;
                                break;
                            case 'S':
                                --y;
                                break;
                        }
                        if (x == a && y == b) {
                            suc = true;
                            break;
                        }
                    }
                    if (x == 0 && y == 0) break;
                    if (x < -20 || y < -20 || x > 30 || y > 30) break;
                }
                output.AppendLine(suc ? "Yes" : "No");
            }
            Console.Write(output.ToString());
        }
    }
}