#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;

void solve() {
    int n, m, L;
    cin >> n >> m >> L;
    multimap<int, int> mp;
    for (int i = 0; i < n; ++i) {
        int l, r;
        cin >> l >> r;
        mp.insert({l, r});
    }
    for (int i = 0; i < m; ++i) {
        int x, v;
        cin >> x >> v;
        mp.insert({x, -v});
    }
    priority_queue<int> q;
    int jmp = 1, ans = 0;
    bool suc = true;
    for (auto [x, v]: mp) {
        if (v < 0) {
            q.push(-v);
        } else {
            while (!q.empty() && jmp < v - x + 2) {
                jmp += q.top(); q.pop();
                ++ans;
            }
            if (jmp < v - x + 2) { suc = false; break; }
        }
    }
    cout << (suc ? ans : -1) << '\n';
}

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int T = 1;
    cin >> T;
    while (T--) solve();
    return 0;
}
