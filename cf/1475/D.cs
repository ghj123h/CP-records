using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
                int[] a = ReadArray<int>(n), b = ReadArray<int>(n);
                List<int> one = new(), two = new();
                for (int i = 0; i < n; ++i) {
                    if (b[i] == 1) one.Add(a[i]);
                    else two.Add(a[i]);
                }
                one.Sort((x, y) => y - x);
                two.Sort((x, y) => y - x);
                int n1 = one.Count, n2 = two.Count;
                one.Add(0); // pivot
                int x = 0, y = 0;
                long sum = 0;
                int ans = 0;
                while (x < n1 && y < n2 && sum < m) {
                    if (two[y] < one[x] + one[x+1] || one[x] + sum >= m) {
                        sum += one[x++];
                        ans++;
                    } else {
                        sum += two[y++];
                        ans += 2;
                    }
                }
                while (x < n1 && sum < m) {
                    sum += one[x++];
                    ans++;
                }
                while (y < n2 && sum < m) {
                    sum += two[y++];
                    ans += 2;
                }
                output.Append(sum < m ? -1 : ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}