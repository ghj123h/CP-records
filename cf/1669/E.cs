using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE;

#if !PROBLEM
SolutionE a = new();
a.Solve();
#endif

namespace TemplateE
{
    internal class SolutionE
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
                string[] s = new string[n];
                for (int i = 0; i < n; ++i) s[i] = sr.ReadLine();
                long ans = 0;
                int[,] mp = new int[11, 11];
                mp[s[0][0] - 'a', s[0][1] - 'a']++;
                for (int i = 1; i < n; ++i) {
                    int f = s[i][0] - 'a', t = s[i][1] - 'a';
                    for (int j = 0; j <= 10; ++j) {
                        if (j != f) ans += mp[j, t];
                        if (j != t) ans += mp[f, j];
                    }
                    mp[f, t]++;
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}