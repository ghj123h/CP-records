using System;
using System.Collections.Generic;
using System.Linq;
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
                long k = Read<long>();
                sr.ReadLine();
                string s = sr.ReadLine();
                long sum = 0;
                int j = 0;
                StringBuilder sb = new(s);
                for (int i = 0; i < n; ++i) {
                    sb[i] = '1';
                    if (s[i] == '0') {
                        if (sum >= k) {
                            sb[i] = '0';
                        } else {
                            int d = i - j;
                            if (sum + d <= k) {
                                sb[j++] = '0';
                                sum += d;
                            } else {
                                sb[(int)(i - (k - sum))] = '0';
                                sum = k;
                            }
                        }
                    }
                }
                output.Append(sb).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}