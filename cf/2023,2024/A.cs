using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TemplateA;

#if !PROBLEM
SolutionA a = new();
a.Solve();
#endif

namespace TemplateA {
    internal class SolutionA {
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
            while (T-- > 0) {
                int n = Read<int>();
                (int, int)[] a = new (int, int)[n];
                for (int i = 0; i < n; ++i) a[i] = (Read<int>(), Read<int>());
                foreach (var (a1, a2) in a.OrderBy(x => Math.Min(x.Item1, x.Item2)).ThenBy(x => Math.Max(x.Item1, x.Item2))) {
                    output.AppendFormat("{0} {1} ", a1, a2);
                }
                output.AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}