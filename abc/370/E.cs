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
            int n = Read<int>(), mod = 998244353;
            long k = Read<long>();
            Dictionary<long, long> dsum = new();
            long[] a = ReadArray<long>(n);
            long ans = 1, pre = 1;
            long sum = 0;
            dsum.Add(0, 1);
            for (int i = 0; i < n; ++i) {
                sum += a[i];
                ans = pre;
                if (dsum.TryGetValue(sum - k, out var ds)) {
                    ans -= ds; ans %= mod;
                    ans += mod; ans %= mod;
                }
                pre = (pre + ans) % mod;
                if (dsum.ContainsKey(sum)) dsum[sum] = (dsum[sum] + ans) % mod;
                else dsum.Add(sum, ans);
            }
            output.Append(ans).AppendLine();
            Console.Write(output.ToString());
        }
    }
}