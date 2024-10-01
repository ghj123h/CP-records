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

namespace TemplateD {
    internal class SolutionD {
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
                long[] a = ReadArray<long>(n);
                long[] d = new long[n + 1], aver = new long[n];
                long sum = a.Sum();
                Array.Fill(aver, sum / n);
                for (int i = 1; i <= sum % n; ++i) aver[n - i]++;
                int p = (int)(n - sum % n - 1);
                long add = 0;
                for (int i = 0; i < n - 1; ++i) {
                    if (p <= i) p = n - 1;
                    if (a[i] + add < aver[i]) {
                        var sub = aver[i] - a[i] - add;
                        d[i + 1] += sub / (n - i - 1); d[n] -= sub / (n - i - 1);
                        int k = (int)(sub % (n - i - 1));
                        if (p - k > i) {
                            d[p + 1]--;
                            p -= k;
                            d[p + 1]++;
                        } else {
                            d[p + 1]--;
                            d[i + 1]++;
                            p = n - 1 - (i - (p - k));
                            d[n]--;
                            d[p + 1]++;
                        }
                        aver[i] = a[i] + add;
                        add = 0;
                    } else {
                        add += a[i] - aver[i];
                    }
                    d[i + 1] += d[i];
                    aver[i + 1] += d[i + 1];
                }
                long min = aver[0], par = aver[0];
                for (int i = 1; i < n; ++i) {
                    min = Math.Min(min, (par += aver[i]) / (i + 1));
                }
                output.Append(aver[^1] - min).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}