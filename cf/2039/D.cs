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
                int n = Read<int>(), m = Read<int>();
                int[] s = ReadArray<int>(m);
                Array.Sort(s);
                int[] ans = new int[n + 1];
                Array.Fill(ans, m - 1);
                bool suc = true;
                for (int i = 1; i <= n; ++i) {
                    if (ans[i] < 0) {
                        suc = false;
                        break;
                    }
                    for (int j = i * 2; j <= n; j += i) {
                        ans[j] = Math.Min(ans[j], ans[i] - 1);
                    }
                    ans[i] = s[ans[i]];
                }
                if (suc) output.AppendJoin(' ', ans.Skip(1)).AppendLine();
                else output.AppendLine("-1");
            }
            Console.Write(output.ToString());
        }
    }
}