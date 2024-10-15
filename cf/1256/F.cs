using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateF;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace TemplateF
{
    internal class SolutionF
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
                string t = sr.ReadLine();
                int[] mp = new int[26];
                foreach (var c in s) mp[c - 'a']++;
                bool rep = mp.Any(p => p > 1);
                foreach (var c in t) mp[c - 'a']--;
                if (mp.Any(p => p != 0)) output.AppendLine("No");
                else if (rep) output.AppendLine("Yes");
                else output.AppendLine((Rev(s) - Rev(t)) % 2 == 0 ? "Yes" : "No");
            }
            Console.Write(output.ToString());
        }

        public static int Rev(string s) {
            int n = s.Length;
            int res = 0;
            for (int i = 0; i < n; ++i) for (int j = i + 1; j < n; ++j) if (s[j] < s[i]) ++res;
            return res;
        }
    }
}