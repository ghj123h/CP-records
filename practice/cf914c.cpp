#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
ll C[1024][1024];
int cnt[1024];

void solve() {
    string s;
    cin >> s;
    int k; cin >> k;
    if (!k) {
        cout << "1\n";
        return;
    }
    int n = s.size(), o = ranges::count(s, '1');
    ll ans = 0;
    for (int v = 1; v < n; ++v) {
        if (cnt[v] == k) {
            for (int i = 0, j = v; i < n && j > 0 && n - i - 1 >= j; ++i) {
                if (s[i] == '0') continue;
                ans += C[n - i - 1][j--];
                ans %= mod;
            }
            if (o >= v) ans = (ans + 1) % mod;
            if (v == 1) --ans;
        }
    }
    cout << ans << '\n';
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int T = 1;
    // cin >> T;
    C[0][0] = 1;
    for (int n = 1; n <= 1000; ++n) {
        C[n][0] = 1;
        for (int k = 1; k <= n; ++k) C[n][k] = (C[n-1][k] + C[n-1][k-1]) % mod;
        cnt[n] = cnt[popcount(1u * n)] + 1;
    }
    while (T--) solve();
    return 0;
}
