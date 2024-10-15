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
                string s = sr.ReadLine();
                int ans = 0;
                int[] b = s.Select(c => c - '0').ToArray();
                int[] d = new int[n + 1];
                for (ans = n; ans >= 2; --ans) {
                    Array.Fill(d, 0);
                    for (int i = 0; i <= n - ans; ++i) {
                        if ((b[i] ^ d[i]) == 0) {
                            d[i] ^= 1;
                            d[i + ans] ^= 1;
                        }
                        d[i + 1] ^= d[i];
                    }
                    bool suc = true;
                    for (int i = n - ans + 1; i < n && suc; ++i) {
                        if ((b[i] ^ d[i]) == 0) suc = false;
                        d[i + 1] ^= d[i];
                    }
                    if (suc) break;
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}