#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, l;
    cin >> n >> l;
    int x, y;
    ll ans = 0;
    if (l < n) {
        // x + y = n or n + 1, y - x = l
        int u = n + (n + l) % 2;
        x = (u - l) / 2, y = (u + l) / 2;
        int mid = (x + y) / 2;
        ll ans = 1LL * x * (x - 1) / 2
            + 1LL * (mid - x + 1) * (mid - x) / 2
            + 1LL * (y - mid - 1) * (y - mid - 2) / 2
            + 1LL * (n - y) * (n - y + 1) / 2;
    } else {
        x = (n + 1) / 2, y = x + l;
        ans = 1LL * (n / 2) * (n / 2 + 1);
        if (n % 2 == 0) ans += n / 2;
    }
    cout << x << ' ' << y << ' ' << ans << '\n';
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
