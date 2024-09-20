#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, q;
    cin >> n >> q;
    vector<int> a(n+1);
    map<int, vector<int>> idx;
    for (int i = 1; i <= n; ++i) {
        cin >> a[i];
        idx[a[i]].push_back(i);
    }

    vector<int> tree(n+1);
    auto lb = [](int x) { return x & -x; };
    auto add = [&](int pos, int val) { for (; pos <= n; pos += lb(pos)) tree[pos] += val; };
    auto query = [&](int pos) { int res = 0; for (; pos; pos -= lb(pos)) res += tree[pos]; return res; };

    for (int i = 1; i <= n; ++i) add(i, 1);
    vector<vector<pair<int, int>>> qu(n+1);
    vector<int> rv(n+1), pos(n+1); // rev: rvalue of current interval; pos: position in qu[k]
    vector<bool> ans(q);
    for (int i = 0; i < q; ++i) {
        int y, x;
        cin >> y >> x;
        qu[x].push_back({y, i});
    }
    ranges::for_each(qu, ranges::sort);
    for (int i = 1; i <= n; ++i) { // i: level
        int M = i == 1 ? n : (n + i - 2) / (i - 1);
        for (int k = 1; k <= M; ++k) if (rv[k] < n) { // k: interval length
            int L = rv[k] + 1, R = n;
            while (L < R) {
                int m = L + (R - L) / 2;
                if (query(m) - query(rv[k]) < k) L = m + 1;
                else R = m;
            }
            for (; pos[k] < qu[k].size() && qu[k][pos[k]].first <= L; ++pos[k])
                ans[qu[k][pos[k]].second] = a[qu[k][pos[k]].first] >= i;
            rv[k] = L;
        }
        if (idx.count(i)) for (auto j: idx[i]) add(j, -1);
    }
    for (auto b: ans) println("{}", b ? "Yes" : "No");
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
