using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TemplateC;

#if !PROBLEM
SolutionC a = new();
a.Solve();
#endif

namespace TemplateC
{
    internal class SolutionC
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
            while (T-- > 0)
            {
                int n = Read<int>(), m = Read<int>();
                sr.ReadLine();
                string[] grid = new string[n];
                for (int i = 0; i < n; ++i) grid[i] = sr.ReadLine();
                bool[,] good = new bool[n, m], vis = new bool[n, m];
                int ans = 0;
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < m; ++j) {
                        if (grid[i][j] != '?') {
                            dfs(i, j);
                            if (!good[i, j]) ++ans;
                        }
                    }
                }
                for (int i = 0; i < n; ++i) {
                    for (int j = 0; j < m; ++j) {
                        if (grid[i][j] == '?') {
                            good[i, j] = true;
                            foreach (var (ii, jj) in neighbors(i, j)) {
                                if (!good[ii,jj]) {
                                    good[i, j] = false;
                                    break;
                                }
                            }
                            if (!good[i, j]) ++ans;
                        }
                    }
                }
                output.Append(ans).AppendLine();
                
                bool dfs(int r, int c) {
                    if (r < 0 || r >= n || c < 0 || c >= m) return true;
                    if (vis[r, c]) return good[r, c];
                    if (grid[r][c] == '?') return false;
                    vis[r, c] = true;
                    int i = r, j = c;
                    switch (grid[r][c]) {
                        case 'U':
                            i = r - 1;
                            break;
                        case 'R':
                            j = c + 1;
                            break;
                        case 'D':
                            i = r + 1;
                            break;
                        case 'L':
                            j = c - 1;
                            break;
                    }
                    return good[r, c] = dfs(i, j);
                }

                IEnumerable<(int, int)> neighbors(int r, int c) {
                    if (r > 0) yield return (r - 1, c);
                    if (c > 0) yield return (r, c - 1);
                    if (r < n - 1) yield return (r + 1, c);
                    if (c < m - 1) yield return (r, c + 1);
                }

            }
            Console.Write(output.ToString());
        }
    }
}