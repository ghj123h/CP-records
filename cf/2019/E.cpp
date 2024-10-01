#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n;
    cin >> n;
    vector<vector<int>> G(n+1);
    for (int i = 1; i < n; ++i) {
        int u, v;
        cin >> u >> v;
        G[u].push_back(v); G[v].push_back(u);
    }
    vector<int> d(n+1), maxd(n+1), f(n+1), son(n+1), top(n+1);
    auto dfs = [&](this auto &&self, int u, int fa) -> void {
        f[u] = fa;
        d[u] = d[fa] + 1;
        for (auto v: G[u]) {
            if (v != fa) {
                self(v, u);
                if (maxd[u] < maxd[v] + 1) {
                    maxd[u] = maxd[v] + 1;
                    son[u] = v;
                }
            }
        }
    };
    auto dfs2 = [&](this auto &&self, int u, int rt) -> void {
        top[u] = rt;
        for (auto v: G[u]) {
            if (v != f[u]) {
                if (v == son[u]) self(v, rt);
                else self(v, v);
            }
        }
    };
    dfs(1, 0);
    dfs2(1, 1);
    queue<int> q;
    int toDel = 0, cur = 1, sz = 0, ans = 0;
    q.push(1);
    while (!q.empty()) {
        auto u = q.front(); q.pop();
        if (d[u] > cur) {
            ans = max(sz, ans);
            cur = d[u];
            sz -= toDel;
            toDel = 0;
        }
        ++sz;
        for (auto v: G[u]) if (v != f[u]) q.push(v);
        if (son[u] == 0) toDel += d[u] - d[top[u]] + 1;
    }
    ans = max(sz, ans);
    cout << n - ans << '\n';
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
