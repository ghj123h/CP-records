using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using TemplateB2;

#if !PROBLEM
SolutionB2 a = new();
a.Solve();
#endif

namespace TemplateB2
{
    internal class SolutionB2
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
            sr.ReadLine();
            while (T-- > 0)
            {
                int n = Read<int>(), k = Read<int>();
                int[] a = ReadArray<int>(n);
                int[] ans = new int[n];
                int[] idx = Enumerable.Range(0, n).OrderBy(i => a[i]).ToArray();
                List<(int, int)> bl = new();
                int cnt = 1, j = 0;
                for (int i = 1; i < n; ++i) {
                    if (a[idx[i]] != a[idx[j]]) {
                        bl.Add((cnt, j));
                        cnt = 1;
                        j = i;
                    } else ++cnt;
                }
                bl.Add((cnt, j));
                bl.Sort((x, y) => y.CompareTo(x));
                j = 0;
                for (; j < bl.Count; ++j) {
                    if (bl[j].Item1 <= k) break;
                    for (int u = 1; u <= k; ++u) ans[idx[bl[j].Item2 + u - 1]] = u;
                }
                List<int> toAdd = new();
                while (j < bl.Count) {
                    for (int i = 0; i < bl[j].Item1; ++i) {
                        toAdd.Add(idx[bl[j].Item2 + i]);
                        if (toAdd.Count == k) {
                            for (int l = 0; l < k; ++l) ans[toAdd[l]] = l + 1;
                            toAdd.Clear();
                        }
                    }
                    ++j;
                }
                output.AppendJoin(' ', ans).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}