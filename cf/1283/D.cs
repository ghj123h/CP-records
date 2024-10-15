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
            int n = Read<int>(), m = Read<int>();
            int[] x = ReadArray<int>(n);
            SortedSet<(int, int)> set = new(x.Select((u, i) => (u, i)));
            SortedSet<int> occ = new(x);
            bool[] plus = new bool[n], minus = new bool[n];
            int d = 1, j = 0;
            long res = 0;
            int[] ans = new int[m];
            List<(int, int)> todel = new();
            while (j < m) {
                foreach (var (u, i) in set) {
                    if (!plus[i]) {
                        if (occ.Contains(u + d)) plus[i] = true;
                        else if (j < m) {
                            res += d; occ.Add(ans[j++] = u + d);
                        }
                    }
                    if (!minus[i]) {
                        if (occ.Contains(u - d)) minus[i] = true;
                        else if (j < m) {
                            res += d; occ.Add(ans[j++] = u - d);
                        }
                    }
                    if (plus[i] && minus[i]) todel.Add((u, i));
                }
                foreach (var t in todel) set.Remove(t);
                todel.Clear();
                ++d;
            }
            output.Append(res).AppendLine();
            output.AppendJoin(' ', ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}