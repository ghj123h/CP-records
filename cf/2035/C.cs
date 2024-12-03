using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
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
                int n = Read<int>();
                int m = n - n % 2;
                bool[] vis = new bool[n + 1];
                int[] ans = new int[n + 1];
                if (BitOperations.IsPow2(m)) {
                    if (n % 2 == 0) {
                        vis[ans[n] = n] = true;
                        vis[ans[n - 1] = n - 1] = true;
                        vis[ans[n - 2] = n - 2] = true;
                        vis[ans[n - 3] = 3] = true;
                        vis[ans[n - 4] = 1] = true;
                    } else {
                        vis[ans[m - 2] = 1] = true;
                        vis[ans[m - 1] = 3] = true;
                        vis[ans[m] = m] = true;
                        vis[ans[n] = n] = true;
                    }
                } else {
                    int u = 1 << BitOperations.Log2((uint)m);
                    int i = BitOperations.TrailingZeroCount(m);
                    vis[ans[m - 2] = u - 1] = true;
                    vis[ans[m - 1] = (u - 1) ^ (1 << i)] = true;
                    vis[ans[m] = m] = true;
                    vis[ans[n] = n] = true;
                }
                for (int i = 1, j = 1; ans[i] == 0; ++i) {
                    while (vis[j]) ++j;
                    ans[i] = j;
                    vis[j] = true;
                }
                int k = 0;
                for (int i = 1; i <= n; ++i) {
                    if (i % 2 == 1) k &= ans[i];
                    else k |= ans[i];
                }
                output.Append(k).AppendLine();
                output.AppendJoin(' ', ans.Skip(1)).AppendLine();
            }
            Console.Write(output.ToString());
        }
    }
}