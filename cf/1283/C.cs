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
            T[] arr = new T[n + 1];
            for (int i = 1; i <= n; ++i) arr[i] = Read<T>();
            return arr;
        }

        public void Solve()
        {
            StringBuilder output = new();
            int n = Read<int>();
            int[] f = ReadArray<int>(n);
            int[] deg = new int[n + 1];
            for (int i = 1; i <= n; ++i) {
                if (f[i] > 0) {
                    --deg[i];
                    ++deg[f[i]];
                }
            }
            List<int> s = new(), t = new(), b = new();
            for (int i = 1; i <= n; ++i) {
                if (deg[i] > 0) t.Add(i);
                else if (deg[i] < 0) s.Add(i);
                else if (f[i] == 0) b.Add(i);
            }
            bool first = true;
            foreach (var (i, j) in t.Zip(s)) {
                if (first) {
                    first = false;
                    if (b.Count > 0) {
                        f[i] = b[0];
                        for (int k = 1; k < b.Count; ++k) f[b[k - 1]] = b[k];
                        f[b[^1]] = j;
                    } else f[i] = j;
                } else f[i] = j;
            }
            if (first) {
                for (int k = 1; k < b.Count; ++k) f[b[k - 1]] = b[k];
                f[b[^1]] = b[0];
            }
            output.AppendJoin(' ', f.Skip(1)).AppendLine();
            Console.Write(output.ToString());
        }
    }
}