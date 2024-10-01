#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n;
    cin >> n;
    vector<int> a(n);
    for (int i = 0; i < n; ++i) cin >> a[i];
    vector<vector<int>> d(n + 1, vector<int>(2048, inf));
    d[0][0] = 0;
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < 2048; ++j) {
            if (d[i][j] < inf) {
                int L = max(j - a[i], 0);
                d[i+1][L] = min(d[i+1][L], d[i][j] + a[i]);
                int R = j + a[i];
                if (R < 2048) d[i+1][R] = min(d[i+1][R], max(d[i][j] - a[i], 0));
            }
        }
    }
    int ans = inf;
    for (int j = 0; j < 2048; ++j) ans = min(ans, j + d[n][j]);
    cout << ans << '\n';
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
