using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateC;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace TemplateC
{
    internal class SolutionC
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
                int[,] mp = new int[n, 5];
                int[] a = new int[n];
                int ans = 0;
                for (int i = 0; i < n; ++i) {
                    string s = sr.ReadLine();
                    foreach (var c in s) mp[i, c - 'a']++;
                }
                for (int j = 0; j < 5; ++j) {
                    for (int i = 0; i < n; ++i) {
                        a[i] = 0;
                        for (int k = 0; k < 5; ++k) a[i] += k == j ? mp[i, k] : -mp[i, k];
                    }
                    Array.Sort(a, (x, y) => y.CompareTo(x));
                    int t = 0;
                    int sum = 0;
                    while (t < n) {
                        if ((sum += a[t]) <= 0) break;
                        ++t;
                    }
                    ans = Math.Max(ans, t);
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}