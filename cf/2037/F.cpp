#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;
constexpr int up = 1e9;

void solve() {
    int k, n, m;
    cin >> n >> m >> k;
    vector<int> h(n), x(n);
    for (int i = 0; i < n; ++i) cin >> h[i];
    for (int i = 0; i < n; ++i) cin >> x[i];
    int L = 1, R = up + 1;
    map<int, int> diff;
    auto check = [&](int mid) -> bool {
        for (int i = 0; i < n; ++i) {
            int dam = (h[i] + mid - 1) / mid;
            if (m >= dam) {
                diff[x[i] - (m - dam)]++;
                diff[x[i] + (m - dam) + 1]--;
            }
        }
        int s = 0;
        for (auto [u, v]: diff) if ((s += v) >= k) return true;
        return false;
    };
    while (L < R) {
        int mid = L + (R - L) / 2;
        diff.clear();
        if (check(mid)) R = mid;
        else L = mid + 1;
    }
    cout << (R == up + 1 ? -1 : L) << '\n';
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int T = 1;
    cin >> T;
    while (T--) solve();
    return 0;
}
