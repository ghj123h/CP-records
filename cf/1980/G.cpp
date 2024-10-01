#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

struct TrieNode {
    int son[2], sz[2];
    TrieNode() { son[0] = son[1] = sz[0] = sz[1] = 0; }
};

class Trie {
    const static int lim = 29;
    vector<TrieNode> nodes;
    int tot = 1;
public:
    Trie(int n) : nodes(n * 30) {}
    void Add(int u) {
        auto p = &nodes[0];
        for (int j = lim; j >= 0; --j) {
            int i = (u >> j) & 1;
            if (p->son[i] == 0) p->son[i] = tot++;
            ++p->sz[i];
            p = &nodes[p->son[i]];
        }
    }
    int MaxXor(int x) {
        auto p = &nodes[0];
        int res = 0;
        for (int j = lim; j >= 0; --j) {
            int i = (x >> j) & 1;
            i ^= 1;
            if (p->son[i] == 0) i ^= 1;
            res = (res << 1) + i;
            p = &nodes[p->son[i]];
        }
        return res ^ x;
    }
    int MaxXor(int x, int ban) {
        auto p = &nodes[0];
        int res = 0;
        for (int j = lim; j >= 0; --j) {
            int i = (x >> j) & 1;
            i ^= 1;
            if (p->son[i] > 0) {
                if (p->sz[i] == 1 && (ban >> j) == (res << 1) + i) i ^= 1;
            } else i ^= 1;
            if (p->son[i] == 0) return 0;
            p = &nodes[p->son[i]];
            res = (res << 1) + i;
        }
        return res ^ x;
    }
};

void solve() {
    int n, m;
    cin >> n >> m;
    vector<vector<pair<int, int>>> G(n+1);
    for (int i = 1; i < n; ++i) {
        int u, v, w;
        cin >> u >> v >> w;
        G[u].push_back({v, w});
        G[v].push_back({u, w});
    }
    vector<int> Xor(n+1), d(n+1);
    auto dfs = [&](this auto &&self, int u, int fa) -> void {
        for (auto [v, w]: G[u]) if (v != fa) {
            Xor[v] = Xor[u] ^ w;
            d[v] = d[u] + 1;
            self(v, u);
        }
    };
    dfs(1, 0);
    vector<Trie> trie(2, Trie(n));
    for (int i = 1; i <= n; ++i) trie[d[i] % 2].Add(Xor[i]);
    int y = 0;
    while (m-- > 0) {
        char c; cin >> c;
        if (c == '^') {
            int u; cin >> u;
            y ^= u;
        } else {
            int v, x;
            cin >> v >> x;
            cout << max(trie[d[v]&1].MaxXor(Xor[v] ^ x, Xor[v]), trie[d[v]&1^1].MaxXor(Xor[v] ^ x ^ y)) << ' ';
        }
    }
    cout << '\n';
}

int main() {
    int T;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    cin >> T;
    // T = 1;
    while (T--) {
        solve();
    }
    return 0;
}
