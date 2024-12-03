#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;
constexpr int maxn = 1e6;

int comp[maxn+1], fact[maxn+1];
ll sum[maxn+1];

void solve() {
    int n;
    cin >> n;
    vector<int> a(n), pr, prod(256, 1);
    for (int i = 0; i < n; ++i) cin >> a[i];
    ll ans = 1;
    auto build = [&](int u) {
        pr.clear();
        while (comp[u]) {
            pr.push_back(fact[u]);
            int f = fact[u];
            while (u % f == 0) u /= f;
        }
        if (u > 1) pr.push_back(u);
        int m = 1 << pr.size();
        for (int i = 1; i < m; ++i) {
            int lb = i & -i;
            prod[i] = prod[i^lb] * pr[countr_zero(1u * i)];
        }
    };
    auto add = [&]() {
        int m = 1 << pr.size();
        for (int i = 1; i < m; ++i) sum[prod[i]] = (sum[prod[i]] + ans) % mod;
    };
    auto work = [&]() {
        int m = 1 << pr.size();
        ans = 0;
        for (int i = 1; i < m; ++i) {
            if (popcount(1u * i) & 1) ans = (ans + sum[prod[i]]) % mod;
            else ans = (ans + mod - sum[prod[i]]) % mod;
        }
    };
    build(a[0]);
    add();
    for (int i = 1; i < n; ++i) {
        build(a[i]);
        work();
        add();
    }
    cout << ans << '\n';
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int T = 1;
    // cin >> T;
    for (int i = 2; i <= maxn; ++i) {
        if (!comp[i]) {
            for (ll j = 1ll * i * i; j <= maxn; j += i) {
                comp[j] = true;
                if (!fact[j]) fact[j] = i;
            }
        }
    }
    while (T--) solve();
    return 0;
}
