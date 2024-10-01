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
                sr.ReadLine();
                string s = sr.ReadLine();
                int m = Read<int>();
                int[] b = ReadArray<int>(m);
                List<int> cur = new();
                int[] mp = new int[26];
                char[] ans = new char[m];
                bool[] vis = new bool[m];
                for (int i = 0; i < m; ++i) if (b[i] == 0) { vis[i] = true; cur.Add(i); }
                foreach (var c in s) mp[c - 'a']++;
                char ch = 'z';
                int z = m;
                while (z > 0) {
                    while (mp[ch - 'a'] < cur.Count) ch--;
                    foreach (var j in cur) {
                        ans[j] = ch;
                        for (int i = 0; i < m; ++i) b[i] -= Math.Abs(i - j);
                    }
                    z -= cur.Count;
                    cur.Clear();
                    for (int i = 0; i < m; ++i) if (!vis[i] && b[i] == 0) {
                            vis[i] = true; cur.Add(i);
                        }
                    ch--;
                }
                output.AppendLine(new string(ans));
            }
            Console.Write(output.ToString());
        }
    }
}