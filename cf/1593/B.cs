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
            string[] pat = { "25", "50", "75", "00" };
            while (T-- > 0)
            {
                string s = sr.ReadLine();
                if (s == "0") {
                    output.AppendLine("0");
                    continue;
                }
                int ans = pat.Min(p => Sub(p));
                output.Append(ans).AppendLine();

                int Sub(string p) {
                    int i = s.Length - 1;
                    while (i >= 0 && s[i] != p[1]) --i;
                    --i;
                    while (i >= 0 && s[i] != p[0]) --i;
                    if (i >= 0) return s.Length - 2 - i;
                    else return s.Length;
                }
            }
            Console.Write(output.ToString());
        }
    }
}