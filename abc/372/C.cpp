#include <bits/stdc++.h>
using namespace std;

using ll = long long;
constexpr int mod = 1000000007;
constexpr int inf = 0x3f3f3f3f;
constexpr ll linf = 0x3f3f3f3f3f3f3f3f;

void solve() {
    int n, q;
    cin >> n >> q;
    string s;
    cin >> s;
    set<int> st;
    for (int i = 2; i < n; ++i) {
        if (s[i-2] == 'A' && s[i-1] == 'B' && s[i] == 'C') st.insert(i);
    }
    while (q--) {
        int x; char c;
        cin >> x >> c;
        s[--x] = c;
        auto p = st.lower_bound(x);
        if (p != st.end() && *p <= x + 2) {
            if (s[*p-2] != 'A' || s[*p-1] != 'B' || s[*p] != 'C') st.erase(p);
        }
        for (int i = x - 2; i <= x; ++i) if (i >= 0 && s[i] == 'A' && s[i+1] == 'B' && s[i+2] == 'C') st.insert(i+2);
        cout << st.size() << '\n';
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
