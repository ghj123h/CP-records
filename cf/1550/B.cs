using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB;

#if !PROBLEM
SolutionB a = new();
a.Solve();
#endif

namespace TemplateB
{
    internal class SolutionB
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
                int n = Read<int>(), a = Read<int>(), b = Read<int>();
                sr.ReadLine();
                string s = sr.ReadLine();
                if (b >= 0) {
                    output.Append((a + b) * n).AppendLine();
                } else {
                    List<int> c = new List<int>();
                    int p = s[0], t = 1;
                    for (int i = 1; i < n; ++i) {
                        if (p != s[i]) {
                            c.Add(t);
                            t = 1; p = s[i];
                        } else ++t;
                    }
                    c.Add(t);
                    output.Append(a * n + b * (c.Count / 2 + 1)).AppendLine();
                }
            }
            Console.Write(output.ToString());
        }
    }
}