#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, m, l;
    cin >> l >> n >> m;
    vector<vector<pair<int, int>>> pt(8, vector<pair<int, int>>());
    vector<int> a(l);

    for (int i = 0; i < l; ++i) cin >> a[i];
    for (int i = 1; i <= n; ++i) for (int j = 1; j <= m; ++j) {
        int u; cin >> u;
        pt[u].push_back({i, j});
    }
    map<int, int> mp;

    auto init = [&](vector<pair<int, int>> &vec) {
        mp.clear();
        for (auto [r, c]: vec) {
            if (mp.count(r)) mp[r] = max(mp[r], c);
            else mp[r] = c;
        }
        int M = 0;
        for (auto p = mp.rbegin(); p != mp.rend(); ++p) {
            p->second = M = max(M, p->second);
        }
    };
    init(pt[a[l-1]]);
    vector<pair<int, int>> tmp(pt[a[l-1]]); tmp.reserve(n * m);
    for (int i = l - 2; i >= 0; --i) {
        tmp.clear();
        for (auto [r, c]: pt[a[i]]) {
            auto p = mp.upper_bound(r);
            if (p == mp.end() || p->second <= c) tmp.push_back({r, c});
        }
        init(tmp);
    }
    if (tmp.size() > 0) cout << "T\n";
    else cout << "N\n";
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
