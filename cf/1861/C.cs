using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            sr.ReadLine();
            while (T-- > 0)
            {
                string s = sr.ReadLine();
                int min = 0, cur = 0, max = 0;
                bool suc = true;
                foreach (var c in s) {
                    if (c == '+') {
                        if (cur++ == max) ++max;
                    } else if (c == '-') {
                        --cur;
                        max = Math.Min(max, cur);
                        min = Math.Min(min, cur);
                    } else {
                        char? r = null;
                        if (max < cur) {
                            r = '0';
                        } else if (min >= cur || cur <= 1) {
                            r = '1';
                        }
                        if (r != null && r != c) {
                            suc = false;
                            break;
                        }
                        if (c == '0') {
                            max = Math.Min(cur - 1, max);
                        } else {
                            min = max = cur;
                        }
                    }
                }
                output.AppendLine(suc ? "Yes" : "No");
            }
            Console.Write(output.ToString());
        }
    }
}