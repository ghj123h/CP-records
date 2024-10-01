#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, q;
    cin >> n >> q;
    vector<int> x(n);
    for (int i = 0; i < n; ++i) cin >> x[i];
    map<ll, int> mp;
    mp[n-1]++;
    for (ll i = 1; i < n; ++i) {
        mp[i * (n - i)] += x[i] - x[i-1] - 1;
        mp[n - 1 + i * (n - i - 1)]++;
    }
    while (q--) {
        ll k;
        cin >> k;
        auto it = mp.find(k);
        cout << (it == mp.end() ? 0 : it->second) << ' ';
    }
    cout << '\n';
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
