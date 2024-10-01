using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateG;

#if !PROBLEM
SolutionG a = new();
a.Solve();
#endif

namespace TemplateG
{
    internal class SolutionG
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
                List<(int, int)>[] G = new List<(int, int)>[n + 1];
                for (int i = 1; i <= n; ++i) G[i] = new();
                for (int i = 1; i < n; ++i) {
                    int u = Read<int>(), v = Read<int>(), w = Read<int>();
                    G[u].Add((v, w)); G[v].Add((u, w));
                }
                int[] xor = new int[n + 1], d = new int[n + 1];
                dfs(1, 0);
                
                void dfs(int u, int fa) {
                    foreach (var (v, w) in G[u]) {
                        if (v != fa) {
                            xor[v] = xor[u] ^ w;
                            d[v] = d[u] + 1;
                            dfs(v, u);
                        }
                    }
                }

                Trie[] trie = new Trie[2];
                trie[0] = new(n);
                trie[1] = new(n);
                for (int i = 1; i <= n; ++i) trie[d[i] % 2].Add(xor[i]);
                int y = 0;
                while (m-- > 0) {
                    sr.ReadLine();
                    char c = (char)sr.Read();
                    if (c == '^') y ^= Read<int>();
                    else {
                        int v = Read<int>(), x = Read<int>();
                        output.Append(Math.Max(
                            trie[d[v] & 1].MaxXor(xor[v] ^ x, xor[v]),
                            trie[d[v] & 1 ^ 1].MaxXor(xor[v] ^ x ^ y))).Append(' ');
                    }
                }
                output.AppendLine();
            }
            Console.Write(output.ToString());
        }
    }

    public class Trie {
        public class TrieNode {
            public int[] son = new int[2], sz = new int[2];
            public TrieNode() { }
        }
        public readonly static int lim = 29;
        public TrieNode[] nodes;
        private int tot = 1;
        public TrieNode Head { get => nodes[0]; }
        public Trie(int n) {
            nodes = new TrieNode[n * 30];
            nodes[0] = new();
        }

        public void Add(int u) {
            var p = Head;
            for (int j = lim; j >= 0; --j) {
                int i = (u >> j) & 1;
                if (p.son[i] == 0) {
                    nodes[p.son[i] = tot++] = new();
                }
                ++p.sz[i];
                p = nodes[p.son[i]];
            }
        }
        public int MaxXor(int x) {
            var p = Head;
            int res = 0;
            for (int j = lim; j >= 0; --j) {
                int i = (x >> j) & 1;
                i ^= 1;
                if (p.son[i] == 0) i ^= 1;
                res = (res << 1) + i;
                p = nodes[p.son[i]];
            }
            return res ^ x;
        }
        public int MaxXor(int x, int ban) {
            var p = Head;
            int res = 0;
            for (int j = lim; j >= 0; --j) {
                int i = (x >> j) & 1;
                i ^= 1;
                if (p.son[i] > 0) {
                    if (p.sz[i] == 1 && (ban >> j) == (res << 1) + i) i ^= 1;
                } else i ^= 1;
                if (p.son[i] == 0) return 0;
                p = nodes[p.son[i]];
                res = (res << 1) + i;
            }
            return res ^ x;
        }
    }
}