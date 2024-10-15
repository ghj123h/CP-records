using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateE;

#if !PROBLEM
SolutionE a = new();
a.Solve();
#endif

namespace TemplateE {
    internal class SolutionE {
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
            int n = Read<int>();
            int[] x = ReadArray<int>(n);
            int[] mp = new int[n + 1];
            foreach (var xx in x) ++mp[xx];
            int cnt = 0, min = 0, max = 0, sum = 0;
            int l = -1, r = 0;
            bool[] vis = new bool[n + 2];
            for (int i = 1; i <= n; ++i) {
                if (mp[i] > 0) { ++min; i += 2; }
            }
            for (int i = 1; i <= n; ++i) {
                if (mp[i] > 0) {
                    if (cnt == 0) l = i;
                    r = i;
                    ++cnt;
                    sum += mp[i];
                } else if (cnt > 0) {
                    max += cnt;
                    if (sum > r - l + 1 && !vis[l - 1]) {
                        --sum;
                        ++max;
                    }
                    if (sum > r - l + 1) {
                        vis[r + 1] = true;
                        ++max;
                    }
                    cnt = 0; sum = 0;
                }
            }
            if (cnt > 0) {
                max += cnt;
                if (sum > r - l + 1 && !vis[l - 1]) {
                    --sum;
                    ++max;
                }
                if (sum > r - l + 1) ++max;
            }
            output.AppendFormat("{0} {1}\n", min, max);
            Console.Write(output.ToString());
        }
    }
}