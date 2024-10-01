using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
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
                int n = Read<int>(), k = Read<int>();
                sr.ReadLine();
                string s = sr.ReadLine();
                //int[][] mp = new int[n][];
                //for (int i = 0; i < n; ++i) mp[i] = new int[26];
                int[] mp = new int[26];
                for (int i = 0; i < n; ++i) mp[s[i] - 'a']++;
                for (int ans = n; ans >= 1; --ans) {
                    int g = Gcd(ans, k), m = ans / g;
                    if (mp.Sum(x => x / m) >= g) {
                        output.Append(ans);
                        break;
                    }
                }
                output.AppendLine();
            }
            Console.Write(output.ToString());
        }

        public static int Gcd(int a, int b) => b == 0 ? a : Gcd(b, a % b);
    }
}