#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int h, w, q;
    cin >> h >> w >> q;
    vector<set<int>> row(h), col(w);
    for (int i = 0; i < h; ++i) for (int j = 0; j < w; ++j) {
        row[i].insert(j);
        col[j].insert(i);
    }
    int tot = h * w;
    auto er = [&](int i, int j) { row[i].erase(j); col[j].erase(i); --tot; };
    while (q--) {
        int i, j;
        cin >> i >> j;
        --i, --j;
        if (row[i].count(j)) er(i, j);
        else {
            auto p = row[i].upper_bound(j);
            if (p != row[i].begin()) er(i, *prev(p));
            if (p != row[i].end()) er(i, *p);
            p = col[j].upper_bound(i);
            if (p != col[j].begin()) er(*prev(p), j);
            if (p != col[j].end()) er(*p, j);
        }
    }
    cout << tot << '\n';
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
