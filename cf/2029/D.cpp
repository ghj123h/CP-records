#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, m;
    cin >> n >> m;
    vector<vector<int>> G(n);
    vector<pair<int, int>> ed(m);
    vector<tuple<int, int, int>> ans;
    for (int i = 0; i < m; ++i) {
        int u, v;
        cin >> u >> v;
        --u, --v;
        ed[i] = {u, v};
        G[u].push_back(i);
        G[v].push_back(i);
    }
    
    vector<int> vise(m), del(m), visv(n);
    auto dfs = [&](this auto &&self, int u) -> int {
        visv[u] = true;
        int res = -1;
        for (auto i: G[u]) if (!vise[i]) {
            vise[i] = true;
            int v = ed[i].first ^ ed[i].second ^ u;
            if (visv[v]) {
                res = v;
                del[i] = true;
                break;
            }
            int r = self(v);
            if (r >= 0) {
                del[i] = true;
                if (r != u) {
                    ans.push_back({u, v, r});
                    res = r;
                    break;
                }
            } 
        }
        visv[u] = false;
        return res;
    };
    for (int i = 0; i < n; ++i) dfs(i);

    vector<int> fa(n);
    ranges::iota(fa, 0);
    auto find = [&](this auto &&self, int u) -> int { return fa[u] == u ? u : fa[u] = self(fa[u]); };
    auto merge = [&](int u, int v) { fa[find(u)] = find(v); };
    for (int i = 0; i < m; ++i) if (!del[i]) {
        merge(ed[i].first, ed[i].second);
    }
    ranges::fill(visv, 0);
    for (int i = 0; i < m; ++i) if (!del[i]) {
        int u = ed[i].first, v = ed[i].second;
        visv[find(u)] = true;
        for (int k = 0; k < n; ++k) if (!visv[find(k)]) {
            visv[fa[k]] = true;
            ans.push_back({u, v, k});
            v = k;
        }
        break;
    }
    cout << ans.size() << '\n';
    for (auto [a, b, c]: ans) cout << a + 1 << ' ' << b + 1 << ' ' << c + 1 << '\n';
}

int main() {
    int T = 1;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    cin >> T;
    while (T--) {
        solve();
    }
    return 0;
}
