#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, m;
    cin >> n >> m;
    vector<vector<int>> G(n + 1);
    while (m--) {
        int u, v;
        cin >> u >> v;
        G[u].push_back(v); G[v].push_back(u);
    }
    int f; cin >> f;
    vector<int> h(f);
    unordered_set<int> s;
    for (int i = 0; i < f; ++i) cin >> h[i];
    int k; cin >> k; int M = 1 << k;
    vector<int> p(k + 1); p[0] = 1;
    for (int i = 1; i <= k; ++i) {
        int j; cin >> j;
        p[i] = h[--j];
        s.insert(j);
    }
    vector<vector<int>> dis(k + 1, vector<int>(n + 1, inf));
    for (int i = 0; i <= k; ++i) {
        queue<int> q;
        dis[i][p[i]] = 0;
        q.push(p[i]);
        while (!q.empty()) {
            int u = q.front(); q.pop();
            for (auto v: G[u]) if (dis[i][v] == inf) {
                dis[i][v] = dis[i][u] + 1;
                q.push(v);
            }
        }
    }
    
    vector<vector<int>> fea(f);
    vector<int> perm;
    perm.reserve(k);
    for (int u = 1; u < M; ++u) {
        perm.clear();
        int v = u;
        while (v) {
            int lb = v & -v;
            perm.push_back(__builtin_ctz(lb) + 1);
            v ^= lb;
        }
        do {
            for (int i = 0; i < f; ++i) {
                if (s.contains(i)) continue;
                int d = dis[0][p[perm[0]]];
                for (int j = 1; j < perm.size(); ++j) d += dis[perm[j-1]][p[perm[j]]];
                d += dis[perm.back()][h[i]];
                if (d == dis[0][h[i]]) fea[i].push_back(u);
            }
        } while (ranges::next_permutation(perm).found);
    }

    vector<int> dp(M); dp[0] = 1;
    for (int i = 0; i < f; ++i) {
        for (int j = M - 1; j; --j) {
            for (auto u: fea[i]) {
                if ((j | u) == j) dp[j] |= dp[j^u];
            }
        }
    }
    int ans = 0;
    for (int i = 1; i < (1 << k); ++i) if (dp[i]) ans = max(ans, __builtin_popcount(i));
    cout << k - ans << '\n';
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
