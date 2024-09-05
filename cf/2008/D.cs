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

        private T[] ReadArray<T>(int n, int startIndex = 0)
            where T : struct, IConvertible
        {
            T[] arr = new T[n + startIndex];
            for (int i = 0; i < n; ++i) arr[i + startIndex] = Read<T>();
            return arr;
        }

        public void Solve()
        {
            StringBuilder output = new();
            int T = Read<int>();
            HashSet<int> set = new();
            while (T-- > 0)
            {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                sr.ReadLine();
                string s = sr.ReadLine();
                int[] F = new int[n];
                Array.Fill(F, -1);
                for (int i = 0; i < n; ++i) if (F[i] < 0)
                    {
                        set.Clear();
                        int u = i, sum = 0;
                        while (set.Add(u))
                        {
                            sum += 1 - (s[u] - '0');
                            u = a[u] - 1;
                        }
                        foreach (var v in set) F[v] = sum;
                    }
                output.AppendJoin(' ', F).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}