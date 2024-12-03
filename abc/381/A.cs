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
            int n = Read<int>(); sr.ReadLine();
            string s = sr.ReadLine();
            // int n = s.Length;
            bool suc = n % 2 == 1;
            for (int i = 0; i < n; ++i) {
                if (i == n / 2 && s[i] != '/') suc = false;
                else if (i > n / 2 && s[i] != '2') suc = false;
                else if (i < n / 2 && s[i] != '1') suc = false;
            }
            output.AppendLine(suc ? "Yes" : "No");
            Console.Write(output.ToString());
        }
    }
}