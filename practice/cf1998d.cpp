#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, m;
    cin >> n >> m;
    vector<vector<int>> G(n+1);
    for (int i = 1; i < n; ++i) G[i].push_back(i + 1);
    while (m--) {
        int u, v;
        cin >> u >> v;
        G[u].push_back(v);
    }
    string ans(n - 1, '1');
    priority_queue<int, vector<int>, greater<int>> q;
    vector<int> dis(n+1, inf);
    q.push(1); dis[1] = 0;
    int md = 0;
    for (int i = 2; i < n - 1; ++i) {
        while (!q.empty() && q.top() < i) {
            auto u = q.top(); q.pop();
            for (auto v: G[u])  {
                if (dis[v] == inf) q.push(v);
                dis[v] = min(dis[v], dis[u] + 1);
                md = max(md, v - dis[v]);
            }
        }
        if (md > i) ans[i-1] = '0';
    }
    println("{}", ans);
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
