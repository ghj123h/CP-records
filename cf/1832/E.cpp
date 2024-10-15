#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, x, y, m, k;
    cin >> n;
    vector<ll> a(n);
    cin >> a[0] >> x >> y >> m >> k;
    for (int i = 1; i < n; ++i) a[i] = (a[i-1] * x + y) % m;
    vector<vector<ll>> b(k + 1, vector<ll>(n));
    b[0][0] = b[1][0] = a[0];
    for (int i = 1; i < n; ++i) {
        b[0][i] = (b[0][i-1] + a[i]) % mod;
        b[1][i] = (b[1][i-1] + b[0][i]) % mod;
    } 
    for (int j = 2; j <= k; ++j) {
        b[j][0] = 0;
        for (int i = 1; i < n; ++i) {
            b[j][i] = (b[j-1][i-1] + b[j][i-1]) % mod;
        }
    }
    ll c = 0;
    for (int i = 0; i < n; ++i) {
        c ^= b[k][i] * (i + 1);
    }
    println("{}", c);
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
