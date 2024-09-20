#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n;
    cin >> n;
    map<int, pair<ll, int>> mp;
    for (int i = 1; i <= n; ++i) {
        int x; cin >> x;
        mp[i] = {x, 1};
    }
    mp[0] = {-inf, 0};
    int q;
    cin >> q;
    ll ans = 0;
    while (q--) {
        int i, t;
        cin >> i >> t;
        auto p = prev(mp.upper_bound(i));
        int j = p->first;
        auto bl = p->second;
        ll s = bl.first + i - j;
        ll res = 0;
        if (s < t) { // move to right
            if (j < i) {
                p->second.second = i - j;
                bl.second -= i - j;
            }
            p = next(p);
            res = 1LL * (t - s) * bl.second;
            while (p != mp.end() && p->second.first < t + bl.second) {
                res += 1LL * (t + bl.second - p->second.first) * p->second.second;
                bl.second += p->second.second;
                p = mp.erase(p);
            }
            bl.first = t;
            mp[i] = bl;
        } else if (s > t) { // move to left
            if (i < j + bl.second - 1) {
                mp[i + 1] = {s + 1, bl.second - (i - j + 1)};
                bl.second = i - j + 1;
            }
            p = prev(mp.erase(p));
            res = 1LL * (s - t) * bl.second;
            while (p->first && p->second.first + p->second.second > t - bl.second + 1) {
                res += 1LL * (p->second.first + p->second.second - (t - bl.second + 1)) * p->second.second;
                bl.second += p->second.second;
                j = p->first;
                p = prev(mp.erase(p));
            }
            bl.first = t - bl.second + 1;
            mp[j] = bl;
        }
        ans += res;
    }
    cout << ans << '\n';
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
