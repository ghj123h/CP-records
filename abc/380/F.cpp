#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;

void solve() {
    int n, m, l;
    cin >> n >> m >> l;
    vector<int> a(n+m+l);
    int M = 0;
    for (int i = 0; i < n; ++i) {
        cin >> a[i];
        M |= 1 << 2 * i;
    }
    for (int i = 0; i < m; ++i) {
        cin >> a[n+i];
        M |= 2 << 2 * (n + i);
    }
    for (int i = 0; i < l; ++i) {
        cin >> a[n+m+i];
    }
    n += m + l;
    vector<array<int, 2>> dp(1 << (2 * n)), vis(1 << (2 * n));

    auto dfs = [&](auto &&self, int mask, int cur) -> int {
        if (vis[mask][cur]) return dp[mask][cur];
        vis[mask][cur] = true;
        auto &r = dp[mask][cur];
        for (int i = 0; i < 2 * n; i += 2) if ((mask >> i & 3) == (1 << cur)) { // 01: taka, 10: ao
            for (int j = 0; j < 2 * n; j += 2) if (!(mask >> j & 3)) { // 00: table
                if (a[j/2] < a[i/2]) {
                    if (!self(self, mask ^ (1 << i + cur) ^ (1 << j + cur), cur ^ 1)) return r = true;
                }
            }
            if (!self(self, mask ^ (1 << i + cur), cur ^ 1)) return r = true;
        }
        return r = false;
    };
    cout << (dfs(dfs, M, 0) ? "Takahashi\n" : "Aoki\n");
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int T = 1;
    // cin >> T;
    while (T--) solve();
    return 0;
}
