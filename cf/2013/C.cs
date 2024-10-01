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
            StringBuilder prev = new();
            int T = Read<int>();
            while (T-- > 0) {
                int n = Read<int>();
                char cur = '0';
                int len = 0;
                while (len < n) { // pad to right
                    output.Append(cur);
                    Console.WriteLine("? {0}", output.ToString());
                    Console.Out.Flush();
                    int res = Read<int>();
                    if (res == 0) {
                        cur = cur == '0' ? '1' : '0';
                        output[len] = cur;
                        Console.WriteLine("? {0}", output.ToString());
                        Console.Out.Flush();
                        res = Read<int>();
                        if (res == 0) {
                            output.Remove(len, 1);
                            break;
                        }
                    }
                    len++;
                }
                cur = '1';
                while (len < n) { // pad to left
                    output.Insert(0, cur);
                    Console.WriteLine("? {0}", output.ToString());
                    Console.Out.Flush();
                    int res = Read<int>();
                    if (res == 0) {
                        cur = cur == '0' ? '1' : '0';
                        output[0] = cur;
                        Console.WriteLine("? {0}", output.ToString());
                        Console.Out.Flush();
                        res = Read<int>();
                        if (res == 0) {
                            output.Remove(0, 1);
                            break;
                        }
                    }
                    len++;
                }
                Console.WriteLine("! {0}{1}", prev.ToString(), output.ToString());
                Console.Out.Flush();
                output.Clear();
            }
        }
    }
}