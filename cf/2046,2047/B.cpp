#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;

void solve() {
    int n;
    cin >> n;
    vector<int> a(n);
    map<int, vector<int>> idx;
    for (int i = 0; i < n; ++i) {
        cin >> a[i];
        idx[a[i]].push_back(i);
    }
    int r = -1, c = 0, k = 0;
    vector<int> ans;
    ans.reserve(n);
    for (auto &[u, v]: idx) {
        if (!c) {
            if (v[0] > r) {
                for (auto &i: v) ans.push_back(u), r = i;
            } else {
                auto p = v.rbegin();
                k = max(*p, r);
                while (*p > r) {
                    ans.push_back(u);
                    ++p;
                }
                c = u;
                while (p != v.rend()) {
                    ans.push_back(u+1);
                    ++p;
                }
            }
        } else {
            if (u == c + 1) {
                auto p = v.rbegin();
                while (p != v.rend() && *p > k) ans.push_back(u), ++p;
                while (p != v.rend()) ans.push_back(u+1), ++p;
            } else {
                for (auto &i: v) ans.push_back(u+1);
            }
        }
    }
    for (int i = 0; i < n; ++i) cout << ans[i] << " \n"[i == n - 1];
}

int main() {
    int T = 1;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    cin >> T;
    while (T--) {
        solve();
    }
    return 0;
}

