using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB2;

#if !PROBLEM
SolutionB2 a = new();
a.Solve();
#endif

namespace TemplateB2
{
    internal class SolutionB2
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
                int n = Read<int>(), m = Read<int>(), q = Read<int>();
                int[] b = ReadArray<int>(m);
                int[] ans = new int[q];
                Array.Sort(b);
                for (int i = 0; i < q; ++i)
                {
                    int a = Read<int>();
                    var j = ~Array.BinarySearch(b, a);
                    if (j == 0)
                    {
                        ans[i] = b[j] - 1;
                    }
                    else if (j == m)
                    {
                        ans[i] = n - b[^1];
                    }
                    else
                    {
                        ans[i] = (b[j] - b[j - 1]) / 2;
                    }
                }
                output.AppendJoin('\n', ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}