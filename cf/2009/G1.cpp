#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

int f[400010];

void solve() {
    multiset<int> window;
    int n, k, q;
    cin >> n >> k >> q;
    vector<int> a(n);
    for (int i = 0; i < n; ++i) cin >> a[i], a[i] += n - i;
    int r = k;
    for (int i = 0; i < r; ++i) {
        window.extract(f[a[i]]++);
        window.insert(f[a[i]]);
    }
    vector<int> ans(n - k + 1);
    ans[0] = k - *window.rbegin();
    while (r < n) {
        if (a[r-k] != a[r]) {
            window.extract(f[a[r-k]]--);
            window.insert(f[a[r-k]]);
            window.extract(f[a[r]]);
            window.insert(++f[a[r]]);
        }
        ++r;
        ans[r-k] = k - *window.rbegin();
    }
    while (q--) {
        int l, r;
        cin >> l >> r;
        println("{}", ans[l-1]);
    }
    for (auto v: a) f[v] = 0;
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
