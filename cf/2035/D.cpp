#include <bits/stdc++.h>
using namespace std;

constexpr int mod = 998244353;
constexpr int maxn = 1e5 + 10;

void solve() {
    int sn = 0, sm = 0;
    int t = 0, n = 1, m = 0, mask = 0, M = n * (n - 1) / 2;
    while (true) {
        sn += n;
        sm += m;
        if (sn > 100'000 || sm > 400'000 || t >= 10000) break;
        ++t;
        /* printf("%d %d\n", n, m);
        for (int k = 0, i = 0; i < n; ++i) for (int j = i + 1; j < n; ++j, ++k)
            if ((mask >> k) & 1) printf("%d %d\n", i, j); */
        if (mask == (((1 << m) - 1) << (M - m))) {
            if (++m > M) {
                M += n++;
                m = 0;
                mask = 0;
            } else {
                mask = (1 << m) - 1;
            }
        } else {
            auto lb = mask & -mask;
            auto r = mask + lb;
            mask = ((mask ^ r) >> __builtin_ctz(lb) + 2) | r;
        }
    }
    printf("%d %d %d %d\n", t, n, m, sm);
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
