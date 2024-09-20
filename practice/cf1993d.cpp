#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, k;
    cin >> n >> k;
    int rem = n % k, bl = (n + k - 1) / k;
    if (!rem) rem = k;
    int m = rem - (rem + 1) / 2;
    vector<int> a(n);
    for (int i = 0; i < n; ++i) cin >> a[i];
    int l = 0, r = ranges::max(a) + 1;
    while (l < r) {
        int mid = l + (r - l) / 2;
        vector<vector<int>> d(bl + 1, vector<int>(rem + 1)); // i: block, j: id in block
        vector<int> mx(rem + 1);
        for (int i = 0; i < bl; ++i) for (int j = 0; j < rem; ++j) {
            d[i+1][j+1] = max(d[i][j+1], mx[j] + (a[i*k+j] > mid));
            mx[j+1] = max(d[i+1][j+1], mx[j+1]);
        }
        if (ranges::max(mx) > m) l = mid + 1;
        else r = mid;
    }
    println("{}", l);
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
