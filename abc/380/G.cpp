#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;

ll Inv(ll a) {
    ll u = 0, v = 1, m = mod;
    while (a > 0) {
        ll t = m / a;
        m -= t * a; swap(a, m);
        u -= t * v; swap(u, v);
    }
    return (u % mod + mod) % mod;
}

void solve() {
    int n, k;
    cin >> n >> k;

    auto lb = [](int u) { return u & -u; };
    auto add = [&](vector<ll> &tr, int pos, int val) { for (; pos < tr.size(); pos += lb(pos)) tr[pos] += val; };
    auto query = [&](vector<ll> &tr, int pos) { int res = 0; for (; pos; pos -= lb(pos)) res += tr[pos]; return res; };

    vector<ll> p(n), w(n+1);
    for (int i = 0; i < n; ++i) cin >> p[i];
    ll ans = 0, sum = 0, sub = 0, dem = Inv(n - k + 1), inv4 = Inv(4);
    for (int i = n - 1; i >= 0; --i) {
        sum += query(w, p[i]);
        add(w, p[i], 1);
    }
    fill(w.begin(), w.end(), 0);
    for (int i = k - 1; i >= 0; --i) {
        sub += query(w, p[i]);
        add(w, p[i], 1);
    }
    ans = sum - sub;
    for (int r = k; r < n; ++r) {
        int l = r - k;
        add(w, p[l], -1);
        sub -= query(w, p[l]);
        add(w, p[r], 1);
        sub += k - query(w, p[r]);
        ans += sum - sub;
        ans %= mod;
    }
    ans *= dem;
    ans %= mod;
    ans += 1ll * k * (k - 1) % mod * inv4 % mod;
    ans %= mod;
    cout << ans << '\n';
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
