#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;

void solve() {
    int n, q;
    cin >> n >> q;
    map<int, int> mp;
    vector<int> tot(n+1);
    mp.insert({0, 0});
    for (int i = 1; i <= n; ++i) mp.insert({i, i}), tot[i] = 1;
    mp.insert({n + 1, n + 1});
    while (q--) {
        int u;
        cin >> u;
        if (u == 1) {
            int x, c;
            cin >> x >> c;
            auto p = prev(mp.upper_bound(x));
            auto l = prev(p), r = next(p);
            tot[p->second] -= r->first - p->first;
            tot[c] += r->first - p->first;
            if (r->second == c) {
                mp.erase(r);
            }
            if (l->second == c) {
                mp.erase(p);
                p = l;
            }
            p->second = c;
        } else {
            int c;
            cin >> c;
            cout << tot[c] << '\n';
        }
    }
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
