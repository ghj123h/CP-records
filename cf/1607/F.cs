using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
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
            StringBuilder output = new();
            int T = Read<int>();
            while (T-- > 0) {
                int n = Read<int>(), m = Read<int>();
                sr.ReadLine();
                string[] grid = new string[n];
                for (int i = 0; i < n; ++i) grid[i] = sr.ReadLine();
                int[,] d = new int[n, m];
                bool[,] vis = new bool[n, m];
                int ans = 0, ansi = 0, ansj = 0;
                Stack<(int, int)> st = new();
                List<(int, int)> tmp = new();
                for (int i = 0; i < n; ++i)
                    for (int j = 0; j < m; ++j) {
                        if (d[i, j] == 0) dfs(i, j);
                        if (ans < d[i, j]) {
                            ans = d[i, j];
                            ansi = i;
                            ansj = j;
                        }
                    }
                output.AppendFormat("{0} {1} {2}\n", ansi + 1, ansj + 1, ans);

                void dfs(int i, int j) {
                    st.Push((i, j)); vis[i, j] = true;
                    while (true) {
                        switch (grid[i][j]) {
                            case 'L': --j; break;
                            case 'R': ++j; break;
                            case 'U': --i; break;
                            case 'D': ++i; break;
                        }
                        if (i < 0 || i == n || j < 0 || j == m) {
                            (i, j) = st.Pop();
                            d[i, j] = 1;
                            vis[i, j] = false;
                            break;
                        } else if (vis[i, j]) {
                            while (true) {
                                var (ii, jj) = st.Pop();
                                tmp.Add((ii, jj));
                                if (ii == i && jj == j) break;
                            }
                            foreach (var (ii, jj) in tmp) {
                                d[ii, jj] = tmp.Count;
                                vis[ii, jj] = false;
                            }
                            tmp.Clear();
                            break;
                        } else if (d[i, j] > 0) break;
                        st.Push((i, j));
                        vis[i, j] = true;
                    }
                    while (st.Count > 0) {
                        var (ii, jj) = st.Pop();
                        d[ii, jj] = d[i, j] + 1;
                        vis[ii, jj] = false;
                        (i, j) = (ii, jj);
                    }
                }
            }
            Console.Write(output.ToString());
        }
    }
}