#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, k;
    cin >> n >> k;
    vector<int> a(n), b(n);
    for (int i = 0; i < n; ++i) cin >> a[i];
    for (int i = 0; i < n; ++i) cin >> b[i];
    int l = 0, r = ranges::max(a);
    auto op = [](int a, int b, int m){ return max((a - m + b - 1) / b, 0); };
    auto check = [&](int m) -> ll {
        return ranges::fold_left(
            views::zip_transform([&](int a, int b){ return op(a, b, m); }, a, b),
            0LL, plus<ll>());
    };
    while (l < r) {
        int m = l + (r - l) / 2;
        if (check(m) > k) l = m + 1;
        else r = m;
    }
    auto dif = k - check(l);
    auto score = ranges::fold_left(
        views::zip_transform([&](int a, int b) -> ll {
                int term = op(a, b, l);
                if (term) {
                    int tail = a - (term - 1) * b;
                    if (tail < 0) tail += b, --term;
                    return 1LL * (a + tail) * term / 2;
                } else return 0;
            }, a, b),
        0LL, plus<ll>());
    score += dif * l;
    println("{}", score);
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
