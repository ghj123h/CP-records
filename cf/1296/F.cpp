#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n;
    cin >> n;
    vector<vector<pair<int, int>>> G(n+1);
    vector<int> e(n+1);
    for (int i = 0; i < n - 1; ++i) {
        int u, v;
        cin >> u >> v;
        G[u].push_back({v, i}); G[v].push_back({u, i});
    }
    vector<int> fa(n+1), dep(n+1), siz(n+1), son(n+1), top(n+1), dfn(n+1), rnk(n+1);
    int cnt = 0;
    dep[1] = 1;
    auto dfs1 = [&](this auto &&self, int u) -> void {
        son[u] = -1;
        siz[u] = 1;
        for (auto &[v, i]: G[u]) if (!dep[v]) {
            dep[v] = dep[u] + 1;
            fa[v] = u;
            e[v] = i;
            self(v);
            siz[u] += siz[v];
            if (son[u] == -1 || siz[v] > siz[son[u]]) son[u] = v;
        }
    };
    auto dfs2 = [&](this auto &&self, int u, int rt) -> void {
        top[u] = rt;
        dfn[u] = ++cnt;
        rnk[cnt] = u;
        if (son[u] == -1) return;
        self(son[u], rt);
        for (auto &[v, _]: G[u]) if (v != fa[u] && v != son[u]) self(v, v);
    };
    dfs1(1);
    dfs2(1, 1);

    int m; cin >> m;
    vector<tuple<int, int, int>> q(m);
    for (int i = 0; i < m; ++i) {
        int a, b, c;
        cin >> a >> b >> c;
        q[i] = {a, b, c};
    }
    vector<int> ans(n - 1, 1000000);
    auto cover = [&](int u, int v, int w) {
        while (top[u] != top[v]) {
            if (dep[top[u]] > dep[top[v]]) {
                int t = fa[top[u]];
                while (u != t) {
                    ans[e[u]] = w;
                    u = fa[u];
                }
            } else {
                int t = fa[top[v]];
                while (v != t) {
                    ans[e[v]] = w;
                    v = fa[v];
                }
            }
        }
        if (dep[u] > dep[v]) {
            while (u != v) {
                ans[e[u]] = w;
                u = fa[u];
            }
        } else {
            while (v != u) {
                ans[e[v]] = w;
                v = fa[v];
            }
        }
    };
    auto check = [&](int u, int v) {
        int res = inf;
        while (top[u] != top[v]) {
            if (dep[top[u]] > dep[top[v]]) {
                int t = fa[top[u]];
                while (u != t) {
                    res = min(res, ans[e[u]]);
                    u = fa[u];
                }
            } else {
                int t = fa[top[v]];
                while (v != t) {
                    res = min(res, ans[e[v]]);
                    v = fa[v];
                }
            }
        }
        if (dep[u] > dep[v]) {
            while (u != v) {
                res = min(res, ans[e[u]]);
                u = fa[u];
            }
        } else {
            while (v != u) {
                res = min(res, ans[e[v]]);
                v = fa[v];
            }
        }
        return res;
    };

    ranges::sort(q, {}, [](const auto &x){ return get<2>(x); });
    for (auto [u, v, w]: q) cover(u, v, w);
    bool suc = true;
    for (auto [u, v, w]: q) {
        if (check(u, v) != w) { suc = false; break; }
    }
    if (!suc) println("-1");
    else {
        for (auto w: ans) print("{} ", w);
        println();
    }
}

int main() {
    int T;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    // cin >> T;
    T = 1;
    while (T--) {
        solve();
    }
    return 0;
}
