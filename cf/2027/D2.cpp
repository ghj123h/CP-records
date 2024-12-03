#include <bits/stdc++.h>
using namespace std;

using ll = long long;
using pii = pair<ll, ll>;
constexpr int mod = 1'000'000'007;
constexpr ll linf = 0x3f3f'3f3f'3f3f'3f3f;

class SimpleSegTree {
    vector<vector<ll>> mn, sum;
    int m, n;
    void Update(int v, int i, int l, int r, const vector<ll> &v1, const vector<ll> &v2) {
        if (l == r && i == l) {
            mn[v][0] = v1[0];
            sum[v][0] = v2[0] % mod;
            for (int j = 1; j < m; ++j) {
                if (v1[j] < mn[v][j-1]) {
                    mn[v][j] = v1[j];
                    sum[v][j] = v2[j] % mod;
                } else if (v1[j] == mn[v][j-1]) {
                    mn[v][j] = v1[j];
                    sum[v][j] = (sum[v][j-1] + v2[j]) % mod;
                } else {
                    mn[v][j] = mn[v][j-1];
                    sum[v][j] = sum[v][j-1];
                }
            }
        } else {
            int mid = l + (r - l) / 2;
            if (i <= mid) Update(v * 2 + 1, i, l, mid, v1, v2);
            else Update(v * 2 + 2, i, mid + 1, r, v1, v2);
            for (int j = 0; j < m; ++j) {
                if (mn[v*2+1][j] == mn[v*2+2][j]) {
                    mn[v][j] = mn[v*2+1][j];
                    sum[v][j] = (sum[v*2+1][j] + sum[v*2+2][j]) % mod;
                } else if (mn[v*2+1][j] < mn[v*2+2][j]) {
                    mn[v][j] = mn[v*2+1][j];
                    sum[v][j] = sum[v*2+1][j];
                } else {
                    mn[v][j] = mn[v*2+2][j];
                    sum[v][j] = sum[v*2+2][j];
                }
            }
        }
    }
    pii Query(int v, int L, int R, int l, int r, int j) {
        if (L > R) return {LLONG_MAX, 0};
        else if (l >= L && r <= R) return {mn[v][j], sum[v][j]};
        int m = l + (r - l) / 2;
        auto [lm, ls] = Query(v * 2 + 1, L, min(R, m), l, m, j);
        auto [rm, rs] = Query(v * 2 + 2, max(L, m + 1), R, m + 1, r, j);
        if (lm < rm) return {lm, ls};
        else if (lm > rm) return {rm, rs};
        else return {lm, ls + rs};
    }

public:
    SimpleSegTree(int n, int m)
        : n(n), m(m), mn(n << 2, vector<ll>(m, linf)), sum(n << 2, vector<ll>(m)) {
    }
    void Update(int i, const vector<ll> &v1, const vector<ll> &v2) { Update(0, i, 0, n - 1, v1, v2); }
    pii Query(int L, int R, int j) { return Query(0, L, R, 0, n - 1, j); }
};

void solve() {
    int n, m;
    cin >> n >> m;
    vector<ll> a(n), b(m), pre(n+1);
    for (int i = 0; i < n; ++i) cin >> a[i], pre[i+1] = pre[i] + a[i];
    for (int i = 0; i < m; ++i) cin >> b[i];
    SimpleSegTree tr(n + 1, m);
    vector<ll> v1(m), v2(m);
    v2[0] = 1;
    tr.Update(0, v1, v2);
    for (int i = 1; i <= n; ++i) {
        for (int j = m - 1; j >= 0; --j) {
            ll v = pre[i] - b[j];
            auto p = lower_bound(pre.begin(), pre.end(), v);
            int k = p - pre.begin();
            if (k >= i) {
                v1[j] = linf;
                v2[j] = 0;
            } else {
                auto [mn, sm] = tr.Query(k, i - 1, j);
                v1[j] = mn + m - j - 1;
                v2[j] = sm;
            }
        }
        tr.Update(i, v1, v2);
    }
    auto [mn, sm] = tr.Query(n, n, m - 1);
    if (mn == linf) cout << "-1\n";
    else cout << mn << ' ' << sm << '\n';
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

/*
5
4 2
9 3 4 3
11 7
1 2
20
19 18
10 2
2 5 2 1 10 3 2 9 9 6
17 9
10 11
2 2 2 2 2 2 2 2 2 2
20 18 16 14 12 10 8 6 4 2 1
1 6
10
32 16 8 4 2 1
*/