#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 998244353;

void solve() {
    int n; cin >> n;
    string s; cin >> s;
    map<char, int> mp;
    for (auto c: s) ++mp[c];
    if (mp.size() > 1) {
        auto p = mp.begin(), q = next(p), it = next(q);
        if (p->second < q->second) swap(p, q); // p: max, q: min
        for (; it != mp.end(); ++it) {
            if (it->second > p->second) p = it;
            if (it->second < q->second) q = it;
        }
        int i = s.find(q->first);
        s[i] = p->first;
    }
    cout << s << '\n';
}

int main() {
    int T = 1;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    cin >> T;
    while (T--) {
        solve();
    }
    return 0;
}

