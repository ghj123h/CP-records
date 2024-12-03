#include <bits/stdc++.h>
using namespace std;

using ll = long long;
using ull = unsigned long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

constexpr int maxn = 200'000 + 10;

ull hsh[maxn], pre[maxn];

void solve() {
    int n, q;
    cin >> n >> q;
    vector<ull> a(n+2), p(n+2);
    for (int i = 1; i <= n; ++i) cin >> a[i], a[i] = hsh[a[i]], p[i] = p[i-1] ^ a[i];
    p[n+1] = p[n];
    map<int, tuple<int, int, bool>> vals;
    string s;
    cin >> s;
    s.insert(s.begin(), '#'); s.push_back('#');
    string::size_type u = -1;
    int j = 1, tot = 0;
    map<int, pair<int, bool>> mp;
    auto check = [&](int l, int k) {
        return (pre[l+k-1] ^ pre[l-1]) == (p[l+k-1] ^ p[l-1]);
    };
    auto erase = [&](map<int, pair<int, bool>>::iterator p) {
        tot -= p->second.second;
        mp.erase(p);
    };
    auto update = [&](map<int, pair<int, bool>>::iterator p) {
        auto ch = check(p->first, p->second.first);
        tot -= p->second.second;
        tot += p->second.second = ch;
    };
    auto insert = [&](int l, int k) {
        auto ch = check(l, k);
        tot += ch;
        mp.insert(make_pair(l, make_pair(k, ch)));
    };
    while ((u = s.find("LR", j)) != string::npos) {
        insert(j, u - j + 1);
        j = u + 1;
    }
    insert(j, n - j + 1);
    while (q--) {
        int u;
        cin >> u;
        if ((s[u] == 'L' && s[u+1] == 'R') || (s[u] == 'R' && s[u-1] == 'L')) {
            auto p = mp.lower_bound(u), q = prev(p);
            q->second.first += p->second.first;
            erase(p);
            update(q);
        }
        if (s[u] == 'L' && s[u-1] == 'L') {
            auto p = prev(mp.lower_bound(u));
            int r = p->first + p->second.first;
            insert(u, r - u);
            p->second.first = u - p->first;
            update(p);
        }
        if (s[u] == 'R' && s[u+1] == 'R') {
            auto p = prev(mp.upper_bound(u));
            int r = p->first + p->second.first;
            insert(u + 1, r - u - 1);
            p->second.first = u - p->first + 1;
            update(p);
        }
        s[u] = 'L' + 'R' - s[u];
        cout << (tot == mp.size() ? "Yes\n" : "No\n");
    }
}

int main() {
    int T;
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    cin >> T;
    // random_device rd;
    mt19937_64 gen(time(0));
    for (int i = 1; i < maxn; ++i) hsh[i] = gen(), pre[i] = pre[i-1] ^ hsh[i];
    // T = 1;
    while (T--) {
        solve();
    }
    return 0;
}
