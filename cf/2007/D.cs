using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;

#if !PROBLEM
SolutionD a = new();
a.Solve();
#endif

namespace Template
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
                int n = Read<int>();
                List<int>[] G = new List<int>[n];
                for (int i = 0; i < n; ++i) G[i] = new();
                for (int i = 1; i < n; ++i)
                {
                    int u = Read<int>(), v = Read<int>();
                    --u; --v;
                    G[u].Add(v); G[v].Add(u);
                }
                sr.ReadLine();
                string s = sr.ReadLine();
                int[] cnt = new int[4];
                for (int i = 1; i < n; ++i) if (G[i].Count == 1)
                    {
                        if (s[i] == '?') ++cnt[2];
                        else cnt[s[i] - '0']++;
                    }
                    else if (s[i] == '?') cnt[3]++;
                int ans = 0;
                if (s[0] != '?')
                {
                    int c = s[0] - '0';
                    ans = cnt[1 - c] + (cnt[2] + 1) / 2;
                }
                else
                {
                    if (cnt[0] != cnt[1]) ans = Math.Max(cnt[0], cnt[1]) + cnt[2] / 2;
                    else
                    {
                        if (cnt[2] % 2 == 0) ans = cnt[2] / 2 + cnt[0];
                        else
                        {
                            ans = cnt[2] / 2 + cnt[3] % 2 + cnt[0];
                        }
                    }
                }
                output.Append(ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}