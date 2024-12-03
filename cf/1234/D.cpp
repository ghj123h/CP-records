#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;

void solve() {
    string s;
    cin >> s;
    array<set<int>, 26> cs;
    for (int i = 0; i < s.size(); ++i) {
        cs[s[i]-'a'].insert(i);
    }
    int q; cin >> q;
    while (q--) {
        int v;
        cin >> v;
        if (v == 1) {
            int p; cin >> p; --p;
            char c; cin >> c;
            cs[s[p]-'a'].erase(p);
            cs[(s[p]=c)-'a'].insert(p);
        } else {
            int l, r; cin >> l >> r; --l;
            int ans = 0;
            for (int i = 0; i < 26; ++i) {
                auto p = cs[i].lower_bound(l);
                if (p != cs[i].end() && *p < r) ++ans;
            }
            cout << ans << '\n';
        }
    }
}

int main() {
    int T = 1;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    // cin >> T;
    while (T--) {
        solve();
    }
    return 0;
}
