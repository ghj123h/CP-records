#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int R = 13, M = 1 << R;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

int pow3[R];

void solve() {
    int n, m, k;
    cin >> n >> m >> k;
    string s;
    vector<pair<int, int>> cd;
    for (int i = 0; i < n; ++i) {
        cin >> s;
        for (int j = 0; j < m; ++j) if (s[j] == '#') cd.push_back({i, j});
    }
    int mx = 0;
    vector<vector<int>> cir(k, vector<int>(R));
    for (int i = 0; i < k; ++i) {
        int x, y, p;
        cin >> x >> y >> p;
        --x; --y;
        for (auto [x0, y0]: cd) {
            int u = (x - x0) * (x - x0) + (y - y0) * (y - y0);
            for (int j = 0; j < R; ++j) if (j * j >= u) { cir[i][j] += p; break; }
        }
        for (int j = 1; j < R; ++j) cir[i][j] += cir[i][j-1];
        for (int j = 1; j < R; ++j) cir[i][j] -= pow3[j];
    }

    vector<vector<ll>> d(k + 1, vector<ll>(M));
    for (int i = 0; i < k; ++i) {
        // d[i+1][0] = d[i][0] + cir[i][0];
        for (int j = 0; j < M; ++j) {
            auto &c = d[i+1][j];
            c = d[i][j] + cir[i][0];
            for (int r = 0; r < R; ++r) if (j & (1 << r)) {
                c = max(c, d[i][j ^ (1 << r)] + cir[i][r]);
            }
        }
    }
    println("{}", ranges::max(d[k]));
}

int main() {
    int T;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    cin >> T;
    pow3[0] = 1;
    for (int i = 1; i < R; ++i) pow3[i] = pow3[i-1] * 3;
    // T = 1;
    while (T--) {
        solve();
    }
    return 0;
}
