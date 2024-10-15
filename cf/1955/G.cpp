#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, m;
    cin >> n >> m;
    vector<vector<int>> a(n, vector<int>(m)), vis(n, vector<int>(m));
    vector<pair<int, int>> d{{1, 0}, {0, 1}};
    for (int i = 0; i < n; ++i) for (int j = 0; j < m; ++j) cin >> a[i][j];
    int ans = 1;
    queue<pair<int, int>> q;

    auto feasible = [&](int f) -> bool {
        while (!q.empty()) q.pop();
        for (auto &r: vis) ranges::fill(r, 0);
        q.push({0, 0});
        vis[0][0] = 1;
        while (!q.empty()) {
            auto [r, c] = q.front(); q.pop();
            for (auto [dr, dc]: d) {
                int i = r + dr, j = c + dc;
                if (i < n && j < m && a[i][j] % f == 0 && !vis[i][j]) {
                    q.push({i, j});
                    vis[i][j] = true;
                }
            }
        }
        return vis[n-1][m-1];
    };

    for (int u = 1; u * u <= a[0][0]; ++u) {
        if (a[0][0] % u == 0) {
            if (feasible(a[0][0] / u)) {
                ans = a[0][0] / u;
                break;
            } else if (feasible(u)) {
                ans = u;
            }
        }
    }
    println("{}", ans);
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
