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
                int n = Read<int>();
                int[] a = ReadArray<int>(n), b = ReadArray<int>(n);
                int m = Read<int>();
                int[] d = ReadArray<int>(m);
                Dictionary<int, int> mb = new();
                HashSet<int> sa = new();
                int cnt = 0;
                for (int i = 0; i < n; ++i) {
                    if (a[i] != b[i]) {
                        if (mb.TryGetValue(b[i], out int k)) mb[b[i]] = k + 1;
                        else mb.Add(b[i], 1);
                        ++cnt;
                    } else {
                        sa.Add(a[i]);
                    }
                }
                bool suc = true;
                for (int i = m - 1; i >= 0 && suc; --i) {
                    if (mb.TryGetValue(d[i], out int k)) {
                        if (k == 1) mb.Remove(d[i]);
                        else mb[d[i]] = k - 1;
                        --cnt;
                    } else if (i == m - 1) {
                        suc = sa.Contains(d[i]);
                    }
                }
                suc = suc && mb.Count == 0;
                output.AppendLine(suc ? "Yes" : "No");
            }
            Console.Write(output.ToString());
        }
    }
}