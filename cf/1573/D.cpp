#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n;
    cin >> n;
    multimap<int, int> mp;
    for (int i = 1; i <= n; ++i) {
        int u; cin >> u;
        if (u) mp.insert({u, i});
    }
    vector<pair<int, int>> ans;
    while (mp.size() > 1) {
        auto mn = mp.begin(), mx = prev(mp.end());
        ans.push_back({mn->second, mx->second});
        if (mn->first > 1) mp.insert({mn->first - 1, mn->second});
        if (mx->first > 1) mp.insert({mx->first - 1, mx->second});
        mp.erase(mn); mp.erase(mx);
    }
    println("{}", ans.size());
    for (auto [i, j]: ans) println("{} {}", i, j);
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
