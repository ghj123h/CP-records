using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateF;

#if !PROBLEM
SolutionF a = new();
a.Solve();
#endif

namespace TemplateF
{
    internal class SolutionF
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
            // somehow it is wrong and I don't know why...
            StringBuilder output = new();
            int mod = 998244353;
            int n = Read<int>(), m = Read<int>(), k = Read<int>();
            if (m == 0) {
                Console.WriteLine("1");
                return;
            }
            SortedDictionary<int, List<int>> alt = new();
            Dictionary<int, long[]> mp = new();
            int[] next = new int[n + 1];
            Init(1);
            while (m-- > 0) {
                int u = Read<int>(), v = Read<int>();
                Init(u+1);
                if (alt.TryGetValue(u, out List<int> list)) list.Add(v);
                else alt.Add(u, new List<int> { v });
                Init(v);
            }
            int d = 1;
            foreach (var (u, _) in alt) {
                while (d <= u) next[d++] = u;
            }
            while (d <= n) next[d++] = alt.First().Key;
            output.Append(dp(1, k)).AppendLine();
            Console.Write(output.ToString());

            void Init(int u) {
                if (u > n) u -= n;
                if (!mp.ContainsKey(u)) {
                    long[] arr = new long[k + 1];
                    Array.Fill(arr, -1);
                    mp.Add(u, arr);
                }
            }
            long dp(int u, int k) {
                if (u > n) u -= n;
                if (mp[u][k] >= 0) return mp[u][k];
                long res;
                int dis = next[u] >= u ? next[u] - u : next[u] - u + n;
                if (dis >= k) res = 1;
                else {
                    k -= dis + 1;
                    res = dp(next[u] + 1, k);
                    foreach (var v in alt[next[u]]) res = (res + dp(v, k)) % mod;
                }
                return mp[u][k] = res;
            }
        }
    }
}