#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, m, q;
    cin >> n >> m >> q;
    vector<tuple<int, int, int>> e(m), qu(q);
    vector<vector<ll>> d(n, vector<ll>(n, linf));
    for (int i = 0; i < m; ++i) {
        int u, v, w;
        cin >> u >> v >> w;
        --u, --v;
        d[u][v] = d[v][u] = w;
        e[i] = {u, v, w};
    }
    for (int i = 0; i < q; ++i) {
        auto &[a, b, c] = qu[i];
        cin >> a >> b;
        --b;
        if (a == 2) {
            cin >> c;
            --c;
        } else {
            auto [u, v, _] = e[b];
            d[u][v] = d[v][u] = linf;
        }
    }
    for (int i = 0; i < n; ++i) d[i][i] = 0;
    for (int k = 0; k < n; ++k) for (int i = 0; i < n; ++i) for (int j = 0; j < n; ++j) d[i][j] = min(d[i][j], d[i][k] + d[k][j]);

    vector<ll> ans;
    for (int i = q - 1; i >= 0; --i) {
        auto [a, b, c] = qu[i];
        if (a == 2) {
            ans.push_back(d[b][c]);
            continue;
        }
        auto [u, v, w] = e[b];
        if (d[u][v] > w) {
            d[u][v] = d[v][u] = w;
            for (int i = 0; i < n; ++i) for (int j = 0; j < n; ++j) {
                d[i][j] = min(d[i][j], d[i][u] + w + d[v][j]);
                d[i][j] = min(d[i][j], d[i][v] + w + d[u][j]);
            }
        }
    }

    for (auto p = ans.rbegin(); p != ans.rend(); ++p) cout << (*p == linf ? -1 : *p) << '\n';
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
