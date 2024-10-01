#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    auto inverse = [&](ll a) -> ll {
        ll u = 0, v = 1, m = mod;
        while (a) {
            ll t = m / a;
            m -= t * a; swap(a, m);
            u -= t * v; swap(u, v);
        }
        return (u % mod + mod) % mod;
    };

    int n, m;
    cin >> n;
    vector<ll> a(n);
    for (int i = 0; i < n; ++i) cin >> a[i];
    vector<ll> p(n);
    for (int i = 0; i < n; ++i) {
        cin >> p[i];
        p[i] = inverse(10000) * p[i] % mod;
    }
    vector<ll> ps(1024), pt(1024);
    vector<ll> *s, *t;
    ps[0] = 1;
    s = &ps; t = &pt;
    for (int i = 0; i < n; ++i) {
        ranges::fill(*t, 0);
        for (int j = 0; j <= 1023; ++j) {
            (*t)[j] += (mod + 1 - p[i]) * (*s)[j] % mod;
            (*t)[j ^ a[i]] += p[i] * (*s)[j] % mod;
            (*t)[j] %= mod; (*t)[j ^ a[i]] %= mod;
        }
        swap(s, t);
    }
    ll ans = 0;
    for (int i = 0; i <= 1023; ++i) ans += (*s)[i] * i * i % mod;
    cout << (ans % mod) << '\n';
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
