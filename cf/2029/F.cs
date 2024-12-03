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
                int r = 0, b = 0, i = 0;
                for (i = 0; i < n; ++i) {
                    if (s[i] == s[(i + 1) % n]) {
                        if (s[i] == 'R') ++r;
                        else ++b;
                    }
                }
                char c;
                bool suc = false;
                i = 0;
                if (r == 0 || b == 0) {
                    c = r == 0 ? 'R' : 'B';
                    int j = s.IndexOf(c);
                    if (j == -1) suc = true;
                    else {
                        List<int> bl = new();
                        if (j > 0) bl.Add(j);
                        int k, rem = n - 1 - j;
                        while ((k = s.IndexOf(c, j + 1)) != -1) {
                            bl.Add(k - j - 1);
                            rem = n - 1 - k;
                            j = k;
                        }
                        if (s[i] != c) bl[0] += rem;
                        else bl.Add(rem);
                        for (i = 0; i < bl.Count; ++i) bl[i] %= 2;
                        if (bl.Count == 1 || bl.Count(v => v == 0) == 1) suc = true;
                    }
                }
                output.Append(suc ? "Yes\n" : "No\n");
            }
            Console.Write(output.ToString());
        }
        public static int[] GetPi(IList<int> s) {
            int n = s.Count;
            int[] res = new int[n];
            for (int i = 1; i < n; ++i) {
                int j = res[i - 1];
                while (j > 0 && s[i] != s[j]) j = res[j - 1];
                if (s[i] == s[j]) ++j;
                res[i] = j;
            }
            return res;
        }
    }
}