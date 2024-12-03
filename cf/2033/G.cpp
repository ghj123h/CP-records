#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int inf = 0x3f3f3f3f;

void solve() {
    int n, bw = 17;
    cin >> n;
    vector<vector<int>> G(n+1);
    for (int i = 1; i < n; ++i) {
        int u, v;
        cin >> u >> v;
        G[u].push_back(v);
        G[v].push_back(u);
    }
    vector<int> dep(n+1), mx(n+1);
    vector<vector<int>> g(bw + 1, vector<int>(n+1)), fa(bw + 1, vector<int>(n + 1));
    auto dfs1 = [&](this auto &&self, int u) -> void {
        mx[u] = dep[u];
        int mx1 = -inf, mx2 = -inf;
        for (auto v: G[u]) {
            if (v == fa[0][u]) continue;
            dep[v] = dep[u] + 1;
            fa[0][v] = u;
            self(v);
            mx[u] = max(mx[u], mx[v]);
            if (mx[v] - dep[u] > mx1) {
                mx2 = mx1; mx1 = mx[v] - dep[u];
            } else if (mx[v] - dep[u] > mx2) mx2 = mx[v] - dep[u];
        }
        for (auto v: G[u]) {
            if (v == fa[0][u]) continue;
            g[0][v] = 1 + (mx[v] - dep[u] == mx1 ? mx2 : mx1);
        }
    };
    auto dfs2 = [&](this auto &&self, int u) -> void {
        for (auto v: G[u]) {
            if (v == fa[0][u]) continue;
            for (int j = 1; j <= bw; ++j) {
                fa[j][v] = fa[j-1][fa[j-1][v]];
                g[j][v] = max(g[j-1][v], g[j-1][fa[j-1][v]] + (1 << j - 1));
            }
            self(v);
        }
    };
    dfs1(1); dfs2(1);
    int q;
    cin >> q;
    while (q--) {
        int u, k;
        cin >> u >> k;
        k = min(k, dep[u]);
        int ans = max(k, mx[u] - dep[u]), rem = 0;
        for (int j = bw; ~j; --j) {
            if (k >> j & 1) {
                k ^= 1 << j;
                ans = max(ans, g[j][u] + rem);
                u = fa[j][u];
                rem |= 1 << j;
            }
        }
        cout << ans << ' ';
    }
    cout << '\n';
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int T = 1;
    cin >> T;
    while (T--) solve();
    return 0;
}
