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
            sr.ReadLine();
            while (T-- > 0)
            {
                string s = sr.ReadLine();
                int n = s.Length;
                bool suc = false;
                for (int i = 1; i < n; ++i) {
                    if (s[i] == s[i-1]) {
                        suc = true;
                        output.AppendLine(s.Substring(i - 1, 2));
                        break;
                    }
                }
                if (!suc) {
                    for (int i = 2; i < n; ++i) {
                        if (s[i] != s[i-1] && s[i-1] != s[i-2] && s[i-2] != s[i]) {
                            suc = true;
                            output.AppendLine(s.Substring(i - 2, 3));
                            break;
                        }
                    }
                }
                if (!suc) output.AppendLine("-1");
            }
            Console.Write(output.ToString());
        }
    }
}