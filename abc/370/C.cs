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

namespace TemplateC {
    internal class SolutionC {
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
            string s = sr.ReadLine(), t = sr.ReadLine();
            List<string> ans = new();
            StringBuilder sb = new(s);
            for (int i = 0; i < s.Length; ++i) {
                if (s[i] > t[i]) {
                    sb[i] = t[i];
                    ans.Add(sb.ToString());
                }
            }
            for (int i = s.Length - 1; i >= 0; --i) {
                if (s[i] < t[i]) {
                    sb[i] = t[i];
                    ans.Add(sb.ToString());
                }
            }
            output.Append(ans.Count).AppendLine();
            output.AppendJoin('\n', ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}