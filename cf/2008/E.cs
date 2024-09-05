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
                if (n % 2 == 0)
                {
                    var odd = s.Where((c, i) => i % 2 == 1).GroupBy(c => c).Select(g => g.Count()).ToArray();
                    var even = s.Where((c, i) => i % 2 == 0).GroupBy(c => c).Select(g => g.Count()).ToArray();
                    output.Append(func(odd, even)).AppendLine();
                }
                else
                {
                    int[] preodd = new int[26], preeven = new int[26];
                    int[] sufodd = new int[26], sufeven = new int[26];
                    for (int i = 0; i < n; ++i)
                    {
                        if (i % 2 == 0) sufodd[s[i] - 'a']++;
                        else sufeven[s[i] - 'a']++;
                    }
                    int ans = int.MaxValue;
                    for (int i = 0; i < n; ++i)
                    {
                        if (i % 2 == 0) sufodd[s[i] - 'a']--;
                        else sufeven[s[i] - 'a']--;
                        ans = Math.Min(ans, func(sufodd.Zip(preodd, (a, b) => a + b), sufeven.Zip(preeven, (a, b) => a + b)));
                        if (i % 2 == 0) preeven[s[i] - 'a']++;
                        else preodd[s[i] - 'a']++;
                    }
                    output.Append(ans + 1).AppendLine();
                }
            }
            Console.Write(output.ToString());
        }

        public static int func(IEnumerable<int> odd, IEnumerable<int> even) => odd.Sum() - odd.Max() + even.Sum() - even.Max();
    }
}