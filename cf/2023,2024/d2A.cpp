#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int a, b;
    cin >> a >> b;
    cout << max(a - max(b - a, 0), 0) << '\n';
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
