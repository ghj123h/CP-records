using System;
using System.Collections.Generic;
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
                int ans = int.MaxValue;
                int l = 0;
                int n = Read<int>();
                sr.ReadLine();
                string s = sr.ReadLine();
                Dictionary<(int, int), int> mp = new();
                mp.Add((0, 0), 0);
                int x = 0, y = 0;
                for (int i = 0; i < n; ++i) {
                    switch (s[i]) {
                        case 'R': ++x; break;
                        case 'L': --x; break;
                        case 'U': --y; break;
                        case 'D': ++y; break;
                    }
                    if (mp.TryGetValue((x, y), out int j)) {
                        if (i - j < ans) {
                            ans = i - j;
                            l = j;
                        }
                        mp[(x, y)] = i + 1;
                    } else mp.Add((x, y), i + 1);
                }
                if (ans < int.MaxValue) output.AppendFormat("{0} {1}\n", l + 1, ans + l + 1);
                else output.AppendLine("-1");
            }
            Console.Write(output.ToString());
        }
    }
}