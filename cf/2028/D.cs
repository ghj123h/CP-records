using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateD;

#if !PROBLEM
SolutionD a = new();
a.Solve();
#endif

namespace TemplateD
{
    internal class SolutionD
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
            string s = "qkj";
            while (T-- > 0)
            {
                int n = Read<int>();
                int[][] p = new int[3][], nxt = new int[3][], idx = new int[3][], ls = new int[3][];
                bool[][] memo = new bool[3][];
                Stack<int> st1 = new(), st2 = new();
                for (int i = 0; i < 3; ++i) {
                    p[i] = ReadArray<int>(n);
                    nxt[i] = new int[n];
                    idx[i] = new int[n];
                    ls[i] = new int[n];
                    memo[i] = new bool[n];
                    for (int j = 0; j < n; ++j) {
                        idx[i][j] = n - p[i][j];
                        nxt[i][n - p[i][j]] = j;
                    }
                    Array.Copy(nxt[i], p[i], n);
                    Array.Fill(nxt[i], n);
                    Array.Fill(ls[i], n);
                    st1.Clear();
                    st2.Clear();
                    for (int j = 0; j < n; ++j) {
                        while (st1.Count > 0 && p[i][st1.Peek()] < p[i][j]) nxt[i][st1.Pop()] = j;
                        while (st2.Count > 0 && p[i][st2.Peek()] > p[i][j]) ls[i][st2.Pop()] = j;
                        st1.Push(j); st2.Push(j);
                    }
                    Array.Fill(memo[i], true);
                }
                List<(int, int)> ans = new();
                bool suc = dfs(0, 0);
                if (suc) {
                    ans.Reverse();
                    output.AppendLine("Yes");
                    output.Append(ans.Count).AppendLine();
                    foreach (var (u, i) in ans) output.AppendFormat("{0} {1}\n", s[i], u);
                } else output.AppendLine("No");

                bool dfs(int u, int v) {
                    if (!memo[v][u]) return false;
                    if (u == n - 1) return true;
                    for (int i = 0; i < 3; ++i) {
                        int j = idx[i][u];
                        j = nxt[i][j];
                        while (j < n && p[i][j] > u) {
                            if (dfs(p[i][j], i)) {
                                ans.Add((p[i][j] + 1, i));
                                return true;
                            }
                            j = ls[i][j];
                        }
                    }
                    return memo[v][u] = false;
                }
            }
            Console.Write(output.ToString());
        }
    }
}