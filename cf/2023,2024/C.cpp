#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

mt19937 rd(time(0));
ll mod, C1, C2, C3;
ll Hash1(ll x,ll y){
    return (x+C1)^(y+C2)+1ll*(x%mod)*(C1%mod)%mod+1ll*(y%mod)*(C2%mod)%mod;
}
ll Hash2(ll x,ll y,ll z){
    return (x+C1)^(y+C2)^(z+C3)+1ll*(x%mod)*(C1%mod)%mod+1ll*(y%mod)*(C2%mod)%mod+1ll*(z%mod)*(C3%mod);
}

void solve() {
    int n, k;
    cin >> n >> k;
    C1 = rd(); C2 = rd(); C3 = rd(); mod = rd();
    vector<int> a(n), b(n);
    vector<vector<int>> G1(n), G2(n);
    for (int i = 0; i < n; ++i) cin >> a[i];
    int m1, m2;
    cin >> m1;
    for (int i = 0; i < m1; ++i) {
        int u, v;
        cin >> u >> v;
        --u, --v;
        G1[u].push_back(v);
    }
    for (int i = 0; i < n; ++i) cin >> b[i];
    cin >> m2;
    for (int i = 0; i < m2; ++i) {
        int u, v;
        cin >> u >> v;
        --u, --v;
        G2[u].push_back(v);
    }
    /* if (k != 2 && k != 4) {
        cout << "No\n";
        return;
    } */
    vector<int> s1(k), s2(k), t1(k), t2(k), h1(k), h2(k), v1(k), v2(k), vis1(n), vis2(n);
    auto dfs1 = [&](this auto &&self, int u, int c) -> void {
        vis1[u] = true;
        if (!a[u]) s1[(c+k-1)%k]++;
        else t1[(c+1)%k]++;
        for (auto v: G1[u]) if (!vis1[v]) self(v, (c + 1) % k);
    };
    auto dfs2 = [&](this auto &&self, int u, int c) -> void {
        vis2[u] = true;
        if (!b[u]) t2[c]++;
        else s2[c]++;
        for (auto v: G2[u]) if (!vis2[v]) self(v, (c + 1) % k);
    };
    dfs1(0, 0); dfs2(0, 0);
    for (int i = 0; i < k; ++i) {
        h1[i] = Hash1(s1[i], t1[i]);
        h2[i] = Hash1(s2[i], t2[i]);
    }
    for (int i = 0; i < k; ++i) {
        v1[i] = Hash2(h1[i],h1[(i+k-1)%k],h1[(i+1)%k]);
        v2[i] = Hash2(h2[i],h2[(i+k-1)%k],h2[(i+1)%k]);
    }
    ranges::sort(v1); ranges::sort(v2);
    bool suc = v1 == v2;
    int sum1 = ranges::count(a, 0), sum2 = ranges::count(b, 0);
    if (sum1 == 0 && sum2 == n) suc = true;
    if (sum1 == n && sum2 == 0) suc = true;
    cout << (suc ? "Yes\n" : "No\n");
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
