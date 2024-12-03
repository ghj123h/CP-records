#include <bits/stdc++.h>
using namespace std;

using ll = long long;

void solve() {
    int n;
    cin >> n;
    vector<int> a(n), r(n), d(n+1);
    vector<pair<int, int>> b;
    b.reserve(n);
    for (int i = 0; i < n; ++i) cin >> a[i];
    b.push_back({-a.back(), 1 - n});
    r[n-1] = n-1;
    for (int i = n - 2; i >= 0; --i) {
        int v = -a[i];
        auto p = ranges::upper_bound(b, make_pair(v, 0));
        if (p == b.end()) {
            b.push_back({v, -i});
            r[i] = i;
        } else r[i] = -p->second;
    }
    int mx = 0;
    int l = 0;
    for (int i = 0; i < n; ++i) {
        ++d[i], --d[r[i]];
        mx = max(mx, a[i]);
        if (!d[i]) {
            while (l <= i) cout << mx << ' ', l++;
            mx = 0;
        }
        d[i+1] += d[i];
    }
    cout << '\n';
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int T;
    cin >> T;
    while (T--) solve();
    return 0;
}
