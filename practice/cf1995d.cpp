#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int k, n, c;
    cin >> n >> c >> k;
    string s;
    cin >> s;
    int U = (1 << c) - 1;
    vector<int> masks, fq(c);
    masks.reserve(n - k + 1);
    vector<bool> bad(1 << c);
    int m = 0;
    for (int i = 0; i < k; ++i) m |= !!++fq[s[i]-'A'] << s[i] - 'A';
    bad[U ^ m] = true;
    for (int r = k; r < n; ++r) {
        if (!--fq[s[r-k]-'A']) m ^= 1 << s[r-k] - 'A';
        m |= !!++fq[s[r]-'A'] << s[r] - 'A';
        bad[U ^ m] = true;
    }
    bad[U ^ (1 << s.back() - 'A')] = true;
    for (int i = U - 1; i >= 0; --i) if (bad[i]) {
        int j = i;
        while (j) {
            int lb = j & -j;
            bad[i^lb] = true;
            j ^= lb;
        }
    }
    auto popcnt = [](int u) -> int { return __builtin_popcount(u); };
    println("{}", ranges::min(views::iota(0, 1 << c) | views::filter([&](int u){ return !bad[u]; }) | views::transform(popcnt)));
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
