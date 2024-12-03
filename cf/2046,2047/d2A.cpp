#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;
set<int> sq;

void solve() {
    int n, sum = 0, ans = 0;
    cin >> n;
    for (int i = 0; i < n; ++i) {
        int u;
        cin >> u;
        ans += sq.count(sum += u);
    }
    cout << ans << '\n';
}

int main() {
    int T = 1;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    cin >> T;
    for (int i = 1; i < 1000; i += 2) sq.insert(i * i);
    while (T--) {
        solve();
    }
    return 0;
}

