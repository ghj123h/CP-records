#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;

void solve() {
    auto lb = [](int u) { return u & -u; };
    auto add = [&](vector<ll> &tr, int pos, int val) { for (; pos < tr.size(); pos += lb(pos)) tr[pos] += val; };
    auto query = [&](vector<ll> &tr, int pos) { ll res = 0; for (; pos; pos -= lb(pos)) res += tr[pos]; return res; };

    int n;
    cin >> n;
    vector<ll> a(n), b(n);
    for (int i = 0; i < n; ++i) cin >> a[i];
    for (int j = 0; j < n; ++j) cin >> b[j];
    map<ll, int> mp;
    for (int i = 0; i < n; ++i) mp.insert({a[i] - b[i], 0}), mp.insert({b[i] - a[i], 0});
    int tot = 0;
    for (auto &[u, v]: mp) v = ++tot;
    vector<ll> tr(mp.size() + 1);
    ll ans = 0;
    for (int i = 0; i < n; ++i) {
        ans += query(tr, mp[a[i]-b[i]] - 1);
        add(tr, mp[b[i]-a[i]], 1);
    }
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
