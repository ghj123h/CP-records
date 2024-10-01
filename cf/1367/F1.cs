using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateF1;

#if !PROBLEM
SolutionF1 a = new();
a.Solve();
#endif

namespace TemplateF1 {
    internal class SolutionF1 {
        private readonly StreamReader sr = new(Console.OpenStandardInput());
        private T Read<T>()
            where T : struct, IConvertible {
            char c;
            dynamic res = default(T);
            dynamic sign = 1;
            while (!sr.EndOfStream && char.IsWhiteSpace((char)sr.Peek())) sr.Read();
            if (!sr.EndOfStream && (char)sr.Peek() == '-') {
                sr.Read();
                sign = -1;
            }
            while (!sr.EndOfStream && char.IsDigit((char)sr.Peek())) {
                c = (char)sr.Read();
                res = res * 10 + c - '0';
            }
            return res * sign;
        }

        private T[] ReadArray<T>(int n)
            where T : struct, IConvertible {
            T[] arr = new T[n];
            for (int i = 0; i < n; ++i) arr[i] = Read<T>();
            return arr;
        }

        public void Solve() {
            StringBuilder output = new();
            int T = Read<int>();
            sr.ReadLine();
            while (T-- > 0) {
                int n = Read<int>();
                int[] a = ReadArray<int>(n);
                var idx = Enumerable.Range(0, n).OrderBy(i => a[i]).ToArray();
                int[] mp = new int[n];
                for (int i = 0; i < n; ++i) mp[idx[i]] = i;
                int[] d = new int[n];
                for (int i = 0; i < n; ++i) {
                    if (mp[i] > 0) d[i] = d[idx[mp[i] - 1]] + 1;
                    else d[i] = 1;
                }
                output.Append(n - d.Max()).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}