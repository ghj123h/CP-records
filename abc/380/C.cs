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
            int n = Read<int>(), k = Read<int>(); sr.ReadLine();
            string s = sr.ReadLine();
            List<int> zero = new(), one = new();
            char c = '0';
            int cnt = 0;
            for (int i = 0; i < s.Length; ++i) {
                if (c != s[i]) {
                    if (c == '1') one.Add(cnt);
                    else zero.Add(cnt);
                    cnt = 1;
                    c = s[i];
                } else ++cnt;
            }
            if (c == '1') one.Add(cnt);
            else zero.Add(cnt);
            for (int i = 0; i < one.Count; ++i) {
                if (k == i + 1) {
                    output.AppendJoin("", Enumerable.Repeat('1', one[i]));
                    output.AppendJoin("", Enumerable.Repeat('0', zero[i]));
                } else {
                    output.AppendJoin("", Enumerable.Repeat('0', zero[i]));
                    output.AppendJoin("", Enumerable.Repeat('1', one[i]));
                }
            }
            if (zero.Count > one.Count) output.AppendJoin("", Enumerable.Repeat('0', zero[^1]));
            Console.Write(output.ToString());
        }
    }
}