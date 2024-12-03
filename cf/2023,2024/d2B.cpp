#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, k;
    cin >> n >> k;
    map<ll, int> mp;
    for (int i = 0; i < n; ++i) {
        int u; cin >> u;
        ++mp[u];
    }
    ll ans = 0, sub = 0;
    for (auto [x, f]: mp) {
        if ((x - sub) * n >= k) {
            ans += k;
            break;
        }
        ans += (x - sub) * n + f;
        k -= (x - sub) * n;
        n -= f;
        sub = x;
    }
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
