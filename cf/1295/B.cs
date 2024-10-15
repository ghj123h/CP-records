using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateB;

#if !PROBLEM
SolutionB a = new();
a.Solve();
#endif

namespace TemplateB
{
    internal class SolutionB
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
                int n = Read<int>(), x = Read<int>();
                sr.ReadLine();
                string s = sr.ReadLine();
                int sum = 0;
                int max = int.MinValue, min = int.MaxValue;
                Dictionary<long, int> mp = new();
                for (int i = 0; i < n; ++i) {
                    sum += (s[i] == '0' ? 1 : -1);
                    if (mp.TryGetValue(sum, out int k)) mp[sum] = k + 1;
                    else mp.Add(sum, 1);
                    max = Math.Max(max, sum);
                    min = Math.Min(min, sum);
                }
                if (sum == 0) {
                    output.Append(mp.ContainsKey(x) ? -1 : 0).AppendLine();
                } else {
                    long t = x, ans = 0;
                    if (x <= max && sum < 0) {
                        t += Math.Max(1L * (min - x - sum - 1) / sum * sum, 0);
                        for (; t <= max; t -= sum) ans += mp[t];
                    }
                    t = x;
                    if (x >= min && sum > 0) {
                        t -= Math.Max(1L * (x - max + sum - 1) / sum * sum, 0);
                        for (; t >= min; t -= sum) ans += mp[t];
                    }
                    if (x == 0) ++ans;
                    output.Append(ans).AppendLine();
                }
            }
            Console.Write(output.ToString());
        }
    }
}