using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateG;

#if !PROBLEM
SolutionG a = new();
a.Solve();
#endif

namespace TemplateG
{
    internal class SolutionG
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
                sr.ReadLine();
                string s = sr.ReadLine();
                int n = s.Length;
                int[] o = new int[n + 1], e = new int[n + 1];
                for (int i = 0; i < n; ++i) {
                    o[i + 1] = o[i];
                    e[i + 1] = e[i];
                    if ("[]".Contains(s[i])) {
                        if (i % 2 == 0) o[i + 1]++;
                        else e[i + 1]++;
                    }
                }
                int q = Read<int>();
                while (q-- > 0) {
                    int l = Read<int>(), r = Read<int>();
                    output.Append(Math.Abs((o[r] - o[l - 1]) - (e[r] - e[l - 1]))).AppendLine();
                }
            }
            Console.Write(output.ToString());
        }
    }
}