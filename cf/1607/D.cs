using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TemplateD;

#if !PROBLEM
SolutionD a = new();
a.Solve();
#endif

namespace TemplateD
{
    internal class SolutionD
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
                sr.ReadLine();
                string s = sr.ReadLine();
                List<int> b = new(), r = new();
                foreach (var (x, c) in a.Zip(s))
                    if (c == 'R') r.Add(x);
                    else b.Add(x);
                r.Sort((a, b) => b.CompareTo(a)); b.Sort();
                int y = 1;
                bool suc = true;
                foreach (var x in b) if (x < y++) suc = false;
                y = n;
                foreach (var x in r) if (x > y--) suc = false;
                output.AppendLine(suc ? "Yes" : "No");
            }
            Console.Write(output.ToString());
        }
    }
}