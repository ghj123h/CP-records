#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

#define Fa(u) find(find, u)

void solve() {
    int n, q;
    cin >> n >> q;
    vector<set<int>> V(n + 1);
    for (int i = 1; i <= n; ++i) V[i].insert(i);
    vector<int> fa(n+1);
    iota(fa.begin(), fa.end(), 0);
    auto find = [&](auto &&self, int u) -> int { return u == fa[u] ? fa[u] : fa[u] = self(self, fa[u]); };
    auto merge = [&](int u, int v) {
        u = Fa(u); v = Fa(v);
        if (u == v) return;
        if (V[u].size() < V[v].size()) {
            for (auto x: V[u]) V[v].insert(x);
            fa[u] = v;
        } else {
            for (auto x: V[v]) V[u].insert(x);
            fa[v] = u;
        }
    };
    while (q--) {
        int a, u, v;
        cin >> a >> u >> v;
        if (a == 1) {
            merge(u, v);
        } else {
            u = Fa(u);
            if (V[u].size() < v) cout << "-1\n";
            else {
                auto p = V[u].rbegin();
                while (--v) ++p;
                cout << *p << '\n';
            }
        }
    }
}

int main() {
    int T;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    // cin >> T;
    T = 1;
    while (T--) {
        solve();
    }
    return 0;
}
