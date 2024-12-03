#include <bits/stdc++.h>
using namespace std;

using ll = long long;

void solve() {
    int n;
    cin >> n;
    vector<int> a(n + 1), mx(n + 1);
    vector<set<int>> st(n + 1);
    for (int i = 2; i <= n; ++i) cin >> a[i];

    auto ins = [&](int i, int v) {
        mx[i] = max(mx[i], v);
        while (!st[i].insert(v).second) {
            st[i].erase(v);
            ++v;
        }
    };

    for (int i = n; i; --i) {
        if (st[i].empty()) ins(a[i], 0);
        else ins(a[i], max(mx[i] + 1, *st[i].rbegin() + (st[i].size() > 1)));
    }
    cout << *st[0].begin() << '\n';
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
